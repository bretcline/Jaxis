import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.channels.*;
import java.nio.charset.*;
import java.io.*;
import java.util.*;

public class Node
{
	//Setup vars
	int nodeNum;
	boolean isActive;
	//boolean initiallyActive;
	boolean startedActive;
	
	//Numbering vars
	int totalNodes;
	int maxPerActive;
	int maxMessages;
	int messageCharSize;
	
	//Checkpointing vars
	Vector<Checkpoint> checkpoints;
	Vector<String> messagesSent;
	Map<Integer, Integer> sent;
	Map<Integer, Integer> rcvd;
	//int checkpointNum;
	int numCheckpoints;
	Vector<Integer> ckptStamps;
	
	//Failing vars
	Vector<String> failSequence;
	int failAfter;
	boolean youFail;
	Vector<Integer> floodOrigins = new Vector<Integer>();
	
	//Networking vars
	Map<Integer, SocketChannel> sockets;
	Selector selector;
	ServerSocketChannel server;
	String[] neighbors;
	int port;
	
	//Buffer vars
	ByteBuffer byteBuff;
	CharBuffer charBuff;
	Charset charset;
	CharsetEncoder encoder;
	CharsetDecoder decoder;
	Vector<String> messageBuffer;
	
	//While-breakers and other node status vars
	boolean runningREB = false;
	boolean flooding = false;
	boolean recovering = false;
	
	//Message-storing vectors
	Queue<String> normalMsgs;
	Queue<String> recoveryMsgs;
	Queue<ConnectionMessage> netBuildingMsgs;
	
	/* Message-reading state:
	 * 0 == normal
	 * 1 == flooding
	 * 2 == flood is finished
	 * 3 == recovery
	 * 4 == netBuilding
	 */
	int msgState;
	
	public Node()
	{
		checkpoints = new Vector<Checkpoint>();
		messagesSent = new Vector<String>();
		sent = new HashMap<Integer, Integer>();
		rcvd = new HashMap<Integer, Integer>();
		numCheckpoints = 0;
		ckptStamps = new Vector<Integer>();
		
		failSequence = new Vector<String>();
		failAfter = -1;
		youFail = false;
		messageBuffer = new Vector<String>();
		
		messageCharSize = 0;
		
		normalMsgs = new LinkedList<String>();
		recoveryMsgs = new LinkedList<String>();
		netBuildingMsgs = new LinkedList<ConnectionMessage>();
		//Set the initial state to network building
		msgState = 4;
	}
	
	public void createNetwork(String[] args)
	{
		boolean buildingNetwork = true;
		/*
		 * When not using a config file: (For testing purposes)
		 * args:
		 * 0 = node number
		 * 1 = max messages per activation
		 * 2 = max messages
		 * 3 = this node's port
		 * 4+ (even) = neighbor machine names
		 * 5+ (odd) = neighbor port numbers
		 * 
		 * When using a config file:
		 * args:
		 * 0 = nodeNum
		 * 1 = input file
		 * 2 = isActive
		 */
		port = -1;
		//Get this node's number
		nodeNum = Integer.parseInt(args[0]);
		//Should the node initially be set to active?
		isActive = false;
		//initiallyActive = false;
		if(args[2].equalsIgnoreCase("active"))
		{
			isActive = true;
			initiallyActive = true;
			startedActive = true;
		}
		boolean begunREB = false;
		
		totalNodes = -1;
		maxPerActive = -1;
		maxMessages = -1;
		
		Vector<String> machineNames = new Vector<String>(); //All the machine names
		Vector<Integer> portNumbers = new Vector<Integer>(); //All the port numbers
		neighbors = null; //The nodeNums of this node's neighbors
		
		//Scanner for user input from console
		Scanner keyboard = new Scanner(System.in);
		
		readInputFile(args, machineNames, portNumbers);

		setupServer();

		createServerAndSockets(machineNames, portNumbers, keyboard);
		
		//buildingNetwork = buildNetwork(buildingNetwork);
		
		//Populate ckptStamps with initial values
		for(int counter = 0; counter < totalNodes; counter++)
		{
			ckptStamps.add(0);
		}
		//Create an initial checkpoint
		Checkpoint initialCP = takeCheckpoint(rcvd, sent, "Initial Checkpoint", ckptStamps,
				numCheckpoints);
	}
	
	public Checkpoint takeCheckpoint(Map<Integer, Integer> incRcvd, Map<Integer, Integer> incSent,
			String incRcvdMessage, Vector<Integer> incTimestamp, int incNum)
	{
		Checkpoint newCP = new Checkpoint(rcvd, sent, "Initial Checkpoint", ckptStamps, numCheckpoints);
		checkpoints.add(newCP);
		System.out.println("Initial Checkpoint: " + newCP.getNum());
		numCheckpoints++;
		
		return newCP;
	}

	private boolean buildNetwork(boolean buildingNetwork) {
		//Run selector, listening for events
		while(buildingNetwork == true)
		{
			try
			{
				//Selector waits for an event
				selector.select();
			}
			catch(IOException i)
			{
				System.out.println("Exception thrown while selector was waiting for events");
				System.exit(-1);
			}
			
			Iterator iter = selector.selectedKeys().iterator();
			while(iter.hasNext())
			{
				SelectionKey key = (SelectionKey)iter.next();
				
				iter.remove();
				
				//Process selection key
				try
				{
					if(key.isValid() == true && key.isAcceptable() == true)
					{
						//A socket is attempting to connect to this node.
						//Accept the client
						SocketChannel client = server.accept();
						client.configureBlocking(false);
						//Register the client with this selector
						client.register(selector, client.validOps());
						
						//Test messaging between nodes
						String messageID = ("C " + nodeNum + " ");
						byteBuff.clear();
						sendMessage(messageID, client, "[in Acceptable]");
					}
					else if(key.isValid() == true && key.isConnectable() == true)
					{
						//A server is available for this node to connect to
						SocketChannel connectingChannel = (SocketChannel)key.channel();
						//Establish the connection
						connectingChannel.finishConnect();
						
						//Send this node's nodeNum to the server neighbor
						String messageID = ("C " + nodeNum + " ");
						byteBuff.clear();
						sendMessage(messageID, connectingChannel, "[in Connectable]");
					}
					// // perhaps this whole else block becomes a "save the message into a vector"
					// // for processing at the top of REB
					else if(key.isValid() == true && key.isReadable() == true)
					{
						//A message is being received from a neighbor.
						SocketChannel sendingChannel = (SocketChannel)key.channel();
						//Clear buffers
						byteBuff.clear();
						charBuff.clear();
						//Read input from neighbor
						int checkClosed = sendingChannel.read(byteBuff);
						if(checkClosed == -1)
						{
							System.out.println("The channel is closed");
							sendingChannel.close();
						}
						else
						{
							//Convert input to string
							byteBuff.flip();
							decoder.decode(byteBuff, charBuff, true);
							charBuff.flip();
							String message = charBuff.toString();
							//For testing purposes, write input to console
							System.out.println(message);
							
							if(message.startsWith("C"))
							{
								//Get the neighbor's node number
								String[] splitMessage = message.split(" ");
								int sendingNode = Integer.parseInt(splitMessage[1]);
								sockets.put(sendingNode, sendingChannel);
							}
						}
					}
				}
				catch(IOException i)
				{
					System.out.println("Exception thrown while selector was processing key");
					i.printStackTrace();
					System.exit(-1);
				}
			}
			
			//Check to see if REB can begin
			int numSocketsConnected = 0;
			for(int counter = 0; counter < neighbors.length; counter++)
			{
				int currentNeighbor = Integer.parseInt(neighbors[counter]);
				if(sockets.get(currentNeighbor) != null)
				{
					numSocketsConnected++;
				}
			}
			
			if(numSocketsConnected == sockets.size())
			{
				buildingNetwork = false;
			}
		}
		return buildingNetwork;
	}

	private void createServerAndSockets(Vector<String> machineNames,
			Vector<Integer> portNumbers, Scanner keyboard) {
		//Server and socket creation block
		try
		{
			/*
			 * Note to self: Create all sockets and servers here and register them with
			 * a single selector.
			 */
			//Create selector to manage events over sockets
			selector = Selector.open();
			
			//Create server
			server = ServerSocketChannel.open();
			server.configureBlocking(false);
			server.socket().bind(new InetSocketAddress(port));
			//Register server with selector
			server.register(selector, SelectionKey.OP_ACCEPT);
			
			//Wait for all servers to be created before creating sockets
			// // If the connect/finishConnect below works, we don't need these two lines.
			//System.out.println("When all servers have been established, press enter");
			//keyboard.nextLine();
			
			//Create sockets
			if(sockets.size() > 0)
			{
				/*
				 * This node will need to open a socket connection for each neighbor with a lower
				 * nodeNum than its own.
				 * Each neighbor with a higher nodeNum than this node's will open the socket connection
				 * itself for this node.
				*/
				
				for(int counter = 0; counter < sockets.size(); counter++)
				{
					if(Integer.parseInt(neighbors[counter]) < nodeNum)
					{
						int currentNeighbor = Integer.parseInt(neighbors[counter]);
						String neighborMachine = machineNames.get(currentNeighbor);
						int neighborPort = portNumbers.get(currentNeighbor);
						
						SocketChannel SC = sockets.get(currentNeighbor);
						SC = SocketChannel.open();
						SC.configureBlocking(false);
						boolean connected = SC.connect(new InetSocketAddress(neighborMachine, neighborPort));
						SC.register(selector, SC.validOps());
						/*
						 * This loop does not behave as we expected.
						 * If you start a node other than Node 0, the program blows up here because finishConnect apparently does not return
						 * false, it throws an exception.
						 */
//						while(connected == false)
//						{
//							connected = SC.finishConnect();
//						}
					}
				}
			}
		}
		catch(IOException i)
		{
			System.out.println("Exception thrown while creating server or sockets");
			i.printStackTrace();
			System.exit(-1);
		}
	}

	private void setupServer() {
		//Set up a selector and server for the node
		selector = null;
		server = null;
		//Set up sockets for communication between this node and its neighbors
		sockets = new HashMap<Integer, SocketChannel>();
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			SocketChannel newSocket = null;
			sockets.put(Integer.parseInt(neighbors[counter]), newSocket);
		}
		
		//Create a buffer for exchanges between neighbors
		messageCharSize = 2 + 3 + 1 + 3 + (totalNodes * 3) + 1;
		byteBuff = ByteBuffer.allocateDirect(messageCharSize * 2);
		//Create a CharBuffer so that exchanges can be in strings
		charBuff = CharBuffer.allocate(messageCharSize);
		//Set up an encoder for conversion from CharBuffer to ByteBuffer
		charset = Charset.forName("UTF-16BE");
		encoder = charset.newEncoder();
		decoder = charset.newDecoder();
		
		//Set initial values for sent and rcvd
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			sent.put(Integer.parseInt(neighbors[counter]), 0);
			rcvd.put(Integer.parseInt(neighbors[counter]), 0);
		}
	}

	private void readInputFile(String[] args, Vector<String> machineNames, Vector<Integer> portNumbers)
	{
		//Read the input file
		try
		{
			BufferedReader br = new BufferedReader(new FileReader(args[1]));
			//Get the first line of the config file
			String line = br.readLine();
			int neighborLines = 0;
			while(line != null)
			{
				String[] lineTokens = line.split("#");
				
				//Ignore everything after the first # sign... so only use token[0]
				//If token[0] does not have any data, ignore the line
				if(lineTokens[0].length() > 0)
				{
					String data = lineTokens[0];
					if(totalNodes == -1)
					{
						totalNodes = Integer.parseInt(data);
					}
					else if(machineNames.size() < totalNodes)
					{
						String[] splitLine = data.split("\t");
						machineNames.add(splitLine[1]);
						portNumbers.add(Integer.parseInt(splitLine[2]));
						//Set this node's port number when you run across it
						if(Integer.parseInt(splitLine[0]) == nodeNum)
						{
							port = Integer.parseInt(splitLine[2]);
						}
					}
					else if(neighborLines < totalNodes)
					{
						String[] splitLine = data.split("\t");
						if(Integer.parseInt(splitLine[0]) == nodeNum)
						{
							neighbors = splitLine[1].split(" ");
						}
						neighborLines++;
					}
					else if(maxPerActive == -1)
					{
						maxPerActive = Integer.parseInt(data);
					}
					else if(maxMessages == -1)
					{
						maxMessages = Integer.parseInt(data);
					}
					else
					{
						failSequence.add(data);
					}
				}
				
				//Get the next line of the config file
				line = br.readLine();
			}
		}
		catch(FileNotFoundException f)
		{
			System.out.println("File not found");
			System.exit(-1);
		}
		catch(IOException i)
		{
			System.out.println("Exception while reading the input file");
			System.exit(-1);
		}
	}
	
	@SuppressWarnings("unchecked")
	// // I don't see restart being checked anywhere.  What is the difference if it is true or false?
	public void runREB(boolean restart)
	{
		// // process the messages that may have been stored in setup
		int numMessagesSent = 0;
		int ckptsSinceRec = 0;
		boolean initiallyActive = false;
		floodOrigins = new Vector<Integer>();
		
		System.out.println("Running REB"); //For testing purposes
		if(failSequence.isEmpty() == false)
		{
			//Continue running REB--there are more nodes that need to fail
			runningREB = true;
			
			//See if this node is the one that needs to fail
			String nextFail = failSequence.remove(0);
			System.out.println("Next Fail = " + nextFail);
			String[] nextFailPieces = nextFail.split(" ");
			if(Integer.parseInt(nextFailPieces[0]) == nodeNum)
			{
				youFail = true;
				failAfter = Integer.parseInt(nextFailPieces[1]);
			}
			
			//Reactive the node if it was initially active at the beginning
			// // What if we are here before ever having met a failure condition?  
			if(startedActive == true)
			{
				initiallyActive = true;
			}
		}
		else
		{
			//All recoveries have finished successfully--end the program
			System.out.println("Ending program");
			return;
		}
		int floodOriginNode = -1;
		// // If runningREB is false, skip all this and just call flood, right?
		while(runningREB == true)
		{
			try
			{
				// // Check for restarting?
				//Send all messages lost after recovery
				int msgLoc = messagesSent.indexOf(checkpoints.lastElement().getLatestMsg());
				if(msgLoc != -1 && msgLoc < messagesSent.size())
				{
					System.out.println("Resending lost messages");
					for(int locCounter = msgLoc; locCounter < messagesSent.size(); locCounter++)
					{
						//Determine who the message goes to
						String messageID = messagesSent.get(locCounter);
						String[] msgComponents = messageID.split("-");
						int nodeWillRcv = Integer.parseInt(msgComponents[0]);
						//Send the message
						SocketChannel sendChannel =
							sockets.get(nodeWillRcv);
						sendMessage(messageID, sendChannel, Integer.toString(nodeWillRcv));
					}
				}
				
				//Selector waits for an event
				selector.select();
			}
			catch(IOException i)
			{
				System.out.println("Exception thrown while selector was waiting for events");
				System.exit(-1);
			}
			
			Iterator iter = selector.selectedKeys().iterator();
			while(iter.hasNext())
			{
				SelectionKey key = (SelectionKey)iter.next();
				
				iter.remove();
				
				//Process selection key
				try
				{
					if(key.isValid() == true && key.isReadable() == true)
					{
						//A message is being received from a neighbor.
						SocketChannel sendingChannel = (SocketChannel)key.channel();
						//Read raw message from neighbor
						byteBuff.clear();
						int checkClosed = sendingChannel.read(byteBuff);
						byteBuff.flip();
						
						if(checkClosed == -1)
						{
							//The neighbor on the other end of this channel has closed
							// // If we are processing a message from a neighbor, how can his channel 
							// // have closed?
							System.out.println("The channel is closed");
							sendingChannel.close();
						}
						else
						{
							//Convert raw message to string
							charBuff.clear();
							decoder.decode(byteBuff, charBuff, true);
							charBuff.flip();
							String msg = charBuff.toString();
							String[] msgTypeCheck = msg.split(" ");
							System.out.println(msg);
							
							if(msgTypeCheck[0].equals("F"))
							{
								//Flood
								// // Are flood messages not stored in messagesSent?
								floodOriginNode = Integer.parseInt(msgTypeCheck[1]);
								runningREB = false;
								break;
							}
							else if(msgTypeCheck[0].equals("M"))
							{
								// // Are "M" messages not stored in messagesSent?
								//Process message
								String[] msgPieces = msgTypeCheck[1].split("-");
								//For testing purposes, print the individual message
								//System.out.println(msgTypeCheck[1] + " received");
								int sendingNum = Integer.parseInt(msgPieces[0]);
								
								//Get timestamps from message
								String[] stamps = msgPieces[1].split("@");
								for(int stampCounter = 1; stampCounter < stamps.length; stampCounter++)
								{
									int thisStamp = Integer.parseInt(stamps[stampCounter]);
									if(ckptStamps.get(stampCounter - 1) < thisStamp)
									{
										ckptStamps.set(stampCounter - 1, thisStamp);
									}
								}
								
								Checkpoint cp = new Checkpoint(rcvd, sent, msgTypeCheck[1], ckptStamps, numCheckpoints);
								checkpoints.add(cp);
								System.out.println("Checkpoint: " + cp.getNum());
								numCheckpoints++;
								ckptsSinceRec++;
								//See if the node fails after taking this checkpoint
								if(youFail == true && ckptsSinceRec >= failAfter)
								{
									//Stop REB and start flooding
									floodOriginNode = nodeNum;
									runningREB = false;
									break;
									
									//ckptsSinceRec = 0;
								}
								else
								{
									//Update the local state
									rcvd.put(sendingNum, rcvd.get(sendingNum) + 1);
									
									if(numMessagesSent < maxMessages)
									{
										//Activate node
										//numMessagesSent = numMessagesSent + activate(numMessagesSent); 
									}
									else
									{
										System.out.println("This node has sent its maximum number of messages");
									}
								}
							}
							
							if(runningREB == false)
							{
								break;
							}
						}
					}
					else if(key.isValid() == true && key.isWritable())
					{
						//Initial activation goes here
						if(initiallyActive == true)
						{
							//numMessagesSent = numMessagesSent + activate(numMessagesSent);
							initiallyActive = false;
						}
					}
				}
				catch(IOException i)
				{
					System.out.println("Exception thrown while selector was processing key");
					i.printStackTrace();
					System.exit(-1);
				}
			}
		}
		
		flood(floodOriginNode);
	}
	
	//Additional class variables
	int numMessagesSent = 0;
	int ckptsSinceRec = 0;
	boolean initiallyActive = false;
	boolean canSendFirstMsg = false;
	int floodOriginNode = -1;
	
	public void loopSelect()
	{		
		while(true)
		{
			getMessagesFromSelector();
			
			if(msgState == 0)
			{
				//This node is in Normal state
				processNormal();
			}
			else if(msgState == 1)
			{
				//This node is in Flooding state
				processFlood(floodOriginNode);
			}
			else if(msgState == 2)
			{
				//This node has finished Flooding
				//Dump normal queue
				processFloodIsFinished();

				/*
				 * The Problem with this:
				 * If node B sends a message to node A after node A has failed and started recovering, node B is going to have to resend
				 * that message to node A.
				 * However, node A could have received the message from node B before node A started recovering. As far as node A can tell,
				 * while it is recovering, it has not received the message from node B. But, once node A finishes recovering, the message
				 * will be in node A's normal queue and node A will process it normally. Node B, on the other hand, will resend the message
				 * thinking that node A never got it. So node A will process the message twice.
				 */
			}
			else if(msgState == 3)
			{
				//This node is in Recovery state
				processRecovery();
			}
			else if(msgState == 4)
			{
				//This node is in netBuilding state
				processNetBuilding();
			}
		}
	}
	
	public void processNormal()
	{
		//Initial activation goes here
		if(initiallyActive == true)
		{
			//Need to send an initial Normal message to get REB going.
			initiallyActive = false;
			activate();
		}
		
		String msg = normalMsgs.poll();
		boolean shouldFlood = false;
		while(msg != null)
		{
			String[] msgTypeCheck = msg.split(" ");
			
			if(msgTypeCheck[0].equals("F"))
			{
				//Flood
				floodOriginNode = Integer.parseInt(msgTypeCheck[1]);
				runningREB = false;
				shouldFlood = true;
				break;
			}
			else if(msgTypeCheck[0].equals("M"))
			{
				// // Are "M" messages not stored in messagesSent?
				//Process message
				String[] msgPieces = msgTypeCheck[1].split("-");
				//For testing purposes, print the individual message
				//System.out.println(msgTypeCheck[1] + " received");
				int sendingNum = Integer.parseInt(msgPieces[0]);
				
				//Get timestamps from message
				String[] stamps = msgPieces[1].split("@");
				for(int stampCounter = 1; stampCounter < stamps.length; stampCounter++)
				{
					int thisStamp = Integer.parseInt(stamps[stampCounter]);
					if(ckptStamps.get(stampCounter - 1) < thisStamp)
					{
						ckptStamps.set(stampCounter - 1, thisStamp);
					}
				}
				
				Checkpoint cp = new Checkpoint(rcvd, sent, msgTypeCheck[1], ckptStamps, numCheckpoints);
				checkpoints.add(cp);
				System.out.println("Checkpoint: " + cp.getNum());
				numCheckpoints++;
				ckptsSinceRec++;
				//See if the node fails after taking this checkpoint
				if(youFail == true && ckptsSinceRec >= failAfter)
				{
					//Stop REB and start flooding
					floodOriginNode = nodeNum;
					runningREB = false;
					shouldFlood = true;
					break;
					
					//ckptsSinceRec = 0;
				}
				else
				{
					//Update the local state
					rcvd.put(sendingNum, rcvd.get(sendingNum) + 1);
					
					if(numMessagesSent < maxMessages)
					{
						//Activate node
						activate(); 
					}
					else
					{
						System.out.println("This node has sent its maximum number of messages");
					}
				}
			}
			//Get the next message, if any
			msg = normalMsgs.poll();
		}
		
		if(shouldFlood == true)
		{
			System.out.println("Change state to Flooding.");
			//Move to Flooding state
			//msgState = 1;
		}
//		else
//		{
//			System.out.println("All stored messages have been acted upon. State remains Normal.");
//		}
	}
	
	public void processFlood(int incFloodOrigin)
	{
		//Called when this node fails or when it receives a flood message.
		//Send flood message to all neighbors that have not already sent this node a flood message
		floodOrigins.add(incFloodOrigin);
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			int currentNeighbor = Integer.parseInt(neighbors[counter]);
			if(floodOrigins.contains(currentNeighbor) == false)
			{
				SocketChannel SC = sockets.get(currentNeighbor);
				String messageID = "F " + nodeNum + " ";
				sendMessage(messageID, SC, neighbors[counter]);
			}
		}
		
		floodOrigins.removeAllElements();
		floodOriginNode = -1;
		
		//Move to FloodIsFinished state
		//msgState = 2;
	}
	
	public void processFloodIsFinished()
	{
		
	}
	
	public void processRecovery()
	{
		
	}
	
	public void processNetBuilding()
	{
		ConnectionMessage CM = netBuildingMsgs.poll();
		while(CM != null)
		{
			String msg = CM.getMessage();
			SocketChannel sendingChannel = CM.getSC();
			String[] splitMessage = msg.split(" ");
			int sendingNode = Integer.parseInt(splitMessage[1]);
			sockets.put(sendingNode, sendingChannel);
			
			//Get the next netBuilding message, if any
			CM = netBuildingMsgs.poll();
		}
		
		//Check to see if REB can begin
		int numSocketsConnected = 0;
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			int currentNeighbor = Integer.parseInt(neighbors[counter]);
			if(sockets.get(currentNeighbor) != null)
			{
				numSocketsConnected++;
			}
		}
		
		if(numSocketsConnected == sockets.size())
		{
			//Move to Normal state
			msgState = 0;
			System.out.println("All netBuilding messages handled. Change state from netBuilding to Normal.");
		}
	}
	
	public void getMessagesFromSelector()
	{
		try 
		{
			selector.select();
		}
		catch (IOException e)
		{
			System.out.println("Error selecting a key in getMessagesFromSelector");
			e.printStackTrace();
		}
		
		Iterator iter = selector.selectedKeys().iterator();
		while(iter.hasNext())
		{
			SelectionKey key = (SelectionKey)iter.next();
			
			iter.remove();
			
			//Process selection key
			try
			{
				if(key.isValid() == true && key.isReadable() == true)
				{
					//A message is being received from a neighbor.
					SocketChannel sendingChannel = (SocketChannel)key.channel();
					//Read raw message from neighbor
					byteBuff.clear();
					int checkClosed = sendingChannel.read(byteBuff);
					byteBuff.flip();
					
					if(checkClosed == -1)
					{
						//The neighbor on the other end of this channel has closed
						System.out.println("The channel is closed");
						sendingChannel.close();
					}
					else
					{
						//Convert raw message to string
						charBuff.clear();
						decoder.decode(byteBuff, charBuff, true);
						charBuff.flip();
						String msg = charBuff.toString();
						String[] msgTypeCheck = msg.split(" ");
						System.out.println("Message from selector: " + msg);
						
						if(msgTypeCheck[0].equals("F") || msgTypeCheck[0].equals("M"))
						{
							//Add the message to the normal storage queue; it is either a normal or flood message
							normalMsgs.offer(msg);
						}
						else if(msgTypeCheck[0].equals("R"))
						{
							//Add the message to the recovery storage queue; it is a recovery message
							recoveryMsgs.offer(msg);
						}
						else if(msgTypeCheck[0].equals("C"))
						{
							//Add the message to the netBuilding storage queue; it is a netBuilding message
							ConnectionMessage CM = new ConnectionMessage(msg, sendingChannel);
							netBuildingMsgs.offer(CM);
						}
					}
				}
				else if(key.isValid() == true && key.isAcceptable() == true)
				{
					//A socket is attempting to connect to this node.
					//Accept the client
					SocketChannel client = server.accept();
					client.configureBlocking(false);
					//Register the client with this selector
					client.register(selector, client.validOps());
					
					//Test messaging between nodes
					String messageID = ("C " + nodeNum + " ");
					byteBuff.clear();
					sendMessage(messageID, client, "[in Acceptable]");
				}
				else if(key.isValid() == true && key.isConnectable() == true)
				{
					//A server is available for this node to connect to
					SocketChannel connectingChannel = (SocketChannel)key.channel();
					//Establish the connection
					connectingChannel.finishConnect();
					
					//Send this node's nodeNum to the server neighbor
					String messageID = ("C " + nodeNum + " ");
					byteBuff.clear();
					sendMessage(messageID, connectingChannel, "[in Connectable]");
				}
			}
			catch(IOException i)
			{
				System.out.println("Error while handling key");
				i.printStackTrace();
			}
		}
	}
	
	public void activate()
	{
		//Random generator to determine whether a neighbor is sent a message
		Random rand = new Random();
		
		isActive = true;
		int numSentThisActive = 0;
		//Send messages to a random set of neighbors
		int numNeighbors = neighbors.length;
		for(int counter = 0; counter < numNeighbors; counter++)
		{
			//Determine if this neighbor will receive a message
			boolean sendMessage = rand.nextBoolean();
			if(sendMessage == true && numSentThisActive < maxPerActive)
			{
				//Prepare and send Message
				prepareMessage(counter);
				numSentThisActive++;
			}
		}
		
		//For testing purposes, ensure that at least one message is sent.
		//Otherwise, system can stop prematurely (no messages sent from any node means premature
		//system stop).
		if(numSentThisActive == 0)
		{
			//System.out.println("Forcing a send");
			//Send a message to this node's last neighbor
			prepareMessage(neighbors.length - 1);
			numSentThisActive++;
		}
		
		isActive = false;
		//System.out.println("Node is deactivating");
	}

	private void prepareMessage(int incNeighborLocation)
	{
		//Send the message
		int nodeWillRcv = Integer.parseInt(neighbors[incNeighborLocation]);
		String stamps = "";
		for(int stampCounter = 0; stampCounter < ckptStamps.size(); stampCounter++)
		{
			stamps = stamps + "@" + ckptStamps.get(stampCounter);
		}
		String messageID = ("M " + nodeNum + "-" + numMessagesSent + stamps + " ");
		SocketChannel sendChannel =
			sockets.get(nodeWillRcv);
		ckptStamps.set(nodeNum, ckptStamps.get(nodeNum) + 1);
		sendChannel.keyFor(selector).attach(ckptStamps);
		sendMessage(messageID, sendChannel, neighbors[incNeighborLocation]);
		
		//Update the local state
		messagesSent.add(messageID);
		sent.put(nodeWillRcv, sent.get(nodeWillRcv) + 1);
		numMessagesSent++;
	}
	
	public void sendMessage(String incMessage, SocketChannel incSendChannel, String incNeighbor)
	{
		charBuff.clear();
		while(incMessage.length() < messageCharSize)
		{
			incMessage = incMessage + " ";
		}
		//System.out.println("Putting " + messageID);
		charBuff.put(incMessage);
		charBuff.flip();
		byteBuff.clear();
		encoder.encode(charBuff, byteBuff, true);
		try
		{
			byteBuff.flip();
			incSendChannel.write(byteBuff);
			//For testing purposes, convert the message into a string
			byteBuff.flip();
			charBuff.clear();
			decoder.decode(byteBuff, charBuff, true);
			charBuff.flip();
			System.out.println("Message " + charBuff.toString() +
					" sent to Node " + incNeighbor);
		}
		catch(IOException i)
		{
			System.out.println("Exception while writing a message");
			i.printStackTrace();
			System.exit(-1);
		}
	}
	
	public void flood(int incFloodOrigin)
	{
		//Called when this node fails or when it receives a flood message.
		//Send flood message to all neighbors that have not already sent this node a flood message
		floodOrigins.add(incFloodOrigin);
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			int currentNeighbor = Integer.parseInt(neighbors[counter]);
			if(floodOrigins.contains(currentNeighbor) == false)
			{
				SocketChannel SC = sockets.get(currentNeighbor);
				String messageID = "F " + nodeNum + " ";
				sendMessage(messageID, SC, neighbors[counter]);
			}
		}
		
		recover();
	}
	
	public void recover()
	{
		//Determine if you're the node that failed
		// // What if you are not?
		if(youFail == true)
		{
			//Roll back to a random checkpoint
			Random rand = new Random(checkpoints.size());
			int rollBackDest = rand.nextInt(checkpoints.size());
			System.out.println("Fail and roll back to: " + rollBackDest);
			rollBackTo(rollBackDest);
		}
		
		System.out.println("Recovering");
		//System.out.println("Current checkpoint: " + checkpoints.lastElement().getTimestamp());
		int iteration = 1;
		int maxIterations = totalNodes - 1;
		boolean iterate = false;
		boolean written = false;
		Map<Integer, Integer> sentReportsRcvd = new HashMap<Integer, Integer>();
		boolean consistentState = false;
		
		while(iteration <= maxIterations && consistentState == false)
		{
			sentReportsRcvd.clear();
			written = false;
			
			//Get the sent data from this node's neighbors
			while(iterate == false)
			{
				try
				{
					//Selector waits for an event
					selector.select();
				}
				catch(IOException i)
				{
					System.out.println("Exception thrown while selector was waiting for events");
					System.exit(-1);
				}
				
				Iterator iter = selector.selectedKeys().iterator();
				while(iter.hasNext())
				{
					SelectionKey key = (SelectionKey)iter.next();
					
					iter.remove();
					
					//Process selection key
					try
					{
						if(key.isValid() == true && key.isReadable() == true)
						{
							//A message is being received from a neighbor.
							SocketChannel sendingChannel = (SocketChannel)key.channel();
							
							for(int counter = 0; counter < sockets.size(); counter++)
							{
								int currentNeighbor = Integer.parseInt(neighbors[counter]);
								if(sentReportsRcvd.containsKey(currentNeighbor) == false)
								{									
									//Read raw message from neighbor
									byteBuff.clear();
									int checkClosed = sendingChannel.read(byteBuff);
									byteBuff.flip();
									
									if(checkClosed == -1)
									{
										//The neighbor on the other end of this channel has closed
										System.out.println("The channel is closed");
										sendingChannel.close();
									}
									else
									{
										//Convert raw message to string
										charBuff.clear();
										decoder.decode(byteBuff, charBuff, true);
										charBuff.flip();
										String msg = charBuff.toString();
										System.out.println(msg);
										
										String[] msgPieces = msg.split(" ");
										if(msgPieces[0].equals("R"))
										{
											if(sentReportsRcvd.containsKey(currentNeighbor) == false)
											{
												String sentReport = msgPieces[1];
												sentReportsRcvd.put(currentNeighbor, Integer.parseInt(sentReport));
											}
										}
									}
								}
							}
						}
						else if(key.isValid() == true && key.isWritable() == true && written == false)
						{
							for(int counter = 0; counter < neighbors.length; counter++)
							{
								//Get the current neighbor's information
								int currentNeighbor = Integer.parseInt(neighbors[counter]);
								SocketChannel SC = sockets.get(currentNeighbor);
								//Prepare the message to be sent
								int sentToNeighbor = sent.get(currentNeighbor);
								String messageID = "R " + Integer.toString(sentToNeighbor) + " ";
								sendMessage(messageID, SC, neighbors[counter]);
							}
							
							written = true;
						}
					}
					catch(IOException i)
					{
						System.out.println("Exception");
						i.printStackTrace();
						System.exit(-1);
					}
					
					if(written == true && sentReportsRcvd.size() == neighbors.length)
					{
						iterate = true;
						break;
					}
				}
			}
			
			//Check for consistency and roll back as required
			boolean neededRollback = makeConsistent(sentReportsRcvd);
			System.out.println("Interation " + iteration + " complete");
			
			iteration++;
			iterate = false;
		}
		
		//When this method is finished, write its current checkpoint to a file and restartREB
		checkpoints.lastElement().ckptWrite("TimestampFile_" + nodeNum + ".txt");
		numCheckpoints = checkpoints.size();
		
		runREB(true);
	}
	
	public boolean makeConsistent(Map<Integer, Integer> incReportedSents)
	{
		//Rolls back this node to its latest consistent checkpoint
		boolean consistent = false;
		boolean needRollback = true;
		int numNoRollbacks = 0;
		
		//Print reported msgs sent for testing purposes
		for(int reportCounter = 0; reportCounter < neighbors.length; reportCounter++)
		{
			/*
			 * An initial check for consistency is used so that the node can roll back to its latest checkpoint if its current state
			 * is inconsistent, then can roll back to its second-latest state if subsequent checkpoints are inconsistent.
			*/
			int neighbor = Integer.parseInt(neighbors[reportCounter]);
			int sentsReported = incReportedSents.get(neighbor);
			System.out.println("Reported " + sentsReported + " msgs sent from Node " + neighbor);
			//System.out.println("Recieved from " + neighbor + ": " + rcvd.get(neighbor));
			
			//If sentsReported for neighbor is less than rcvd for neighbor, roll back.
			if(sentsReported < rcvd.get(neighbor))
			{
				rollBackTo(checkpoints.lastElement().getNum());
			}
			else
			{
				consistent = true;
				//The node was consistent with this neighbor without having to roll back.
				numNoRollbacks++;
			}
			
			while(consistent == false)
			{
				//Keep rolling back until this node is consistent with the neighbor being looking at.
				//If sentsReported for neighbor is less than rcvd for neighbor, roll back.
				if(sentsReported < rcvd.get(neighbor))
				{
					if(checkpoints.size() >= 2)
					{
						rollBackTo(checkpoints.get(checkpoints.size() - 2).getNum());
					}
					else
					{
						System.out.println("No more checkpoints to roll back to.");
						consistent = true;
					}
				}
				else
				{
					consistent = true;
				}
			}
		}
		
		if(numNoRollbacks >= neighbors.length)
		{
			//This node did not have to roll back at all.
			needRollback = false;
		}
		
		return needRollback;
	}
	
	public void rollBackTo(int incCkptNum)
	{
		//Rolls this node back to the specified checkpoint
		System.out.println("Roll back to: " + incCkptNum);
		Checkpoint newState = checkpoints.get(incCkptNum);
		//Sets the state of the node to that stored in the checkpoint
		for(int counter = 0; counter < neighbors.length; counter++)
		{
			int currentNeighbor = Integer.parseInt(neighbors[counter]);
			rcvd.put(currentNeighbor, newState.getRcvd(currentNeighbor));
			sent.put(currentNeighbor, newState.getSent(currentNeighbor));
		}
		
		//Remove all checkpoints after the one rolled back to
		for(int counter = incCkptNum + 1; counter < checkpoints.size();)
		{
			checkpoints.remove(counter);
		}
	}
	
	public class Checkpoint
	{
		//Vector<String> ckptMessages;
		Map<Integer, Integer> ckptRcvd;
		Map<Integer, Integer> ckptSent;
		String messageJustRcvd;
		Vector<Integer> timestamp;
		int checkpointNum;
		
		public Checkpoint()
		{
			//ckptMessages = new Vector<String>();
			ckptRcvd = new HashMap<Integer, Integer>();
			ckptSent = new HashMap<Integer, Integer>();
			messageJustRcvd = "";
			timestamp = new Vector<Integer>();
			checkpointNum = -1;
		}
		
		public Checkpoint(Map<Integer, Integer> incRcvd,
				Map<Integer, Integer> incSent, String incRcvdMessage, Vector<Integer> incTimestamp, int incNum)
		{
			ckptRcvd = new HashMap<Integer, Integer>();
			ckptSent = new HashMap<Integer, Integer>();
			messageJustRcvd = "";
			timestamp = new Vector<Integer>();
			
			for(int counter = 0; counter < neighbors.length; counter++)
			{
				int currentNeighbor = Integer.parseInt(neighbors[counter]);
				
				ckptSent.put(currentNeighbor, incSent.get(currentNeighbor));
				ckptRcvd.put(currentNeighbor, incRcvd.get(currentNeighbor));
			}
			
			messageJustRcvd = incRcvdMessage;
			
			for(int counter = 0; counter < totalNodes; counter++)
			{
				timestamp.add(incTimestamp.get(counter));
			}
			
			checkpointNum = incNum;
		}
		
		public int getNum()
		{
			return checkpointNum;
		}
		
		public Vector<Integer> getTimestamp()
		{
			return timestamp;
		}
		
		public int getTimestampFor(int incIndex)
		{
			return timestamp.get(incIndex);
		}
		
		public int getRcvd(int incIndex)
		{
			return ckptRcvd.get(incIndex);
		}
		
		public Map<Integer, Integer> getRcvd()
		{
			return ckptRcvd;
		}
		
		public int getSent(int incIndex)
		{
			return ckptSent.get(incIndex);
		}
		
		public Map<Integer, Integer> getSent()
		{
			return ckptSent;
		}
		
		public String getLatestMsg()
		{
			return messageJustRcvd;
		}
		
//		public Vector<String> getMessages(boolean includeLatest)
//		{
//			Vector<String> returnVector = ckptMessages;
//			if(includeLatest == true)
//			{
//				returnVector.add(messageJustRcvd);
//			}
//			
//			return returnVector;
//		}
		
		public void print()
		{
			System.out.println("Printing checkpoint " + timestamp);
			for(int counter = 0; counter < neighbors.length; counter++)
			{
				int currentNeighbor = Integer.parseInt(neighbors[counter]);
				System.out.println("Received from " + neighbors[counter] +
						": " + ckptRcvd.get(currentNeighbor));
				System.out.println("Sent to " + neighbors[counter] +
						": " + ckptSent.get(currentNeighbor));
			}
		}
		
		public void ckptWrite(String fileName)
		{
			try
			{
				BufferedWriter bw = new BufferedWriter(new FileWriter(fileName, true));
				
				for(int writeCounter = 0; writeCounter < timestamp.size(); writeCounter++)
				{
					System.out.print(timestamp.get(writeCounter) + " ");
					bw.append(timestamp.get(writeCounter) + " ");
				}
				System.out.println();
				bw.append("\n");
				
				bw.flush();
				bw.close();
			}
			catch(IOException i)
			{
				System.out.println("Exception while writing checkpoint to file");
			}
		}
	}
	
	public class ConnectionMessage
	{
		String theMessage = "";
		SocketChannel theSC = null;
		
		public ConnectionMessage(String incMessage, SocketChannel incSC)
		{
			theMessage = incMessage;
			theSC = incSC;
		}
		
		public String getMessage()
		{
			return theMessage;
		}
		
		public SocketChannel getSC()
		{
			return theSC;
		}
	}
	
	public static void main(String[] args)
	{
		/*
		 * Notes on running:
		 * Start the initially active process last
		 */
		Node thisNode = new Node();
		thisNode.createNetwork(args);
		thisNode.loopSelect();
		//thisNode.runREB(false);
		//For testing purposes: force manual end of program
//		while(true)
//		{
//			
//		}
	}
}
