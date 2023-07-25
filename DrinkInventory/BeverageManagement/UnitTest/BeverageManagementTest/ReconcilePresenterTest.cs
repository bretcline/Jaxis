using System;
using System.Linq;
using BeverageManagement.Forms.Reconcile;
using Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IUIDataItems;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.BeverageManagementTest
{
    [TestFixture]
    public class ReconcilePresenterTest
    {
        private FakeReconcileView m_view;
        private IBLManagerFactory m_factory;

        [SetUp]
        public void SetUp()
        {
            Services.Clock = new FakeClock();
            ((FakeClock)Services.Clock).Now = new DateTime(2011, 1, 31);
            m_factory = FakeManagerFactory.Recreate();
            CreateView();
        }

        private void CreateView()
        {
            m_view = new FakeReconcileView(m_factory);
            m_view.FireLoad();
        }

        [Test]
        public void ShowsTicket()
        {
            CreateTicket("Ticket 1", "100", new DateTime(2011, 1, 27, 8, 0, 0, 0), "Joe's", 10, "Table 1");
            CreateView();
            Assert.AreEqual(1, m_view.Tickets.ToList().Count, "One ticket");
        }

        private void CreateTicket(string _comments, string _checkNumber, DateTime _ticketDate, string _establishment,
            int _guestCount, string _customerTable, params POSTicketItem[] _items)
        {
            var ticket = m_factory.ManagePOSTickets().Create();
            ticket.Comments = _comments;
            ticket.CheckNumber = _checkNumber;
            ticket.TicketDate = _ticketDate;
            ticket.Establishment = _establishment;
            ticket.GuestCount = _guestCount;
            ticket.CustomerTable = _customerTable;

            foreach (var item in _items)
            {
                var ticketItem = ticket.NewItem();
                ticketItem.Comment = item.Comment;
                ticketItem.Description = item.Description;
                ticketItem.ItemStatus = item.ItemStatus;
                ticketItem.Price = item.Price;
                ticketItem.Quantity = item.Quantity;
                ticketItem.Reconciled = item.Reconciled;
            }

            m_factory.ManagePOSTickets().Save(ticket);
        }

        [Test]
        public void OpensRecipesForm()
        {
            m_view.FireRecipesClick();
            AssertHelper.HasValueIsTrue(FakeRecipesView.Shown);
        }

        [Test]
        public void OpensAliasesForm()
        {
            m_view.FireAliasesClick();
            AssertHelper.HasValueIsTrue(FakeTicketItemAliasesView.Shown);
        }

        [Test]
        public void ShowsTicketFields()
        {
            CreateTicket("Ticket 1", "100", new DateTime(2011, 1, 27, 8, 0, 0, 0), "Joe's", 10, "Table 1");
            CreateView();
            var ticket = (from t in m_view.Tickets where t.Comments == "Ticket 1" select t).FirstOrDefault();
            Assert.IsNotNull(ticket);
            Assert.AreEqual("Ticket 1", ticket.Comments);
            Assert.AreEqual("100", ticket.CheckNumber);
            Assert.AreEqual("01/27/2011 08:00 AM", ticket.DateTime);
            Assert.AreEqual("Joe's", ticket.Establishment);
            Assert.AreEqual("10", ticket.GuestCount);
            Assert.AreEqual("Table 1", ticket.CustomerTable);
        }

        [Test]
        public void Refreshes()
        {
            CreateTicket("Ticket 1", "100", new DateTime(2011, 1, 27, 8, 0, 0, 0), "Joe's", 10, "Table 1");
            CreateView();
            DeleteTickets();
            m_view.FireRefreshClick();
            Assert.IsEmpty(m_view.Tickets.ToList());
        }

        [Test]
        public void ShowsLinesItems()
        {
            CreateTicket("Ticket 1", "100", new DateTime(2011, 1, 27, 8, 0, 0, 0), "Joe's", 10, "Table 1",
                new POSTicketItem(), new POSTicketItem());
            CreateView();
            var ticket = (from t in m_view.Tickets select t).FirstOrDefault();
            Assert.AreEqual(2, ticket.Items.ToList().Count);
        }

        [Test]
        public void ShowsLineItemFields()
        {
            CreateTicket("Comments", "", DateTime.Now, "", 0, "", new POSTicketItem { Comment = "Comment", 
                Description = "Description", Price = 4.50M, ItemStatus = PosStatus.Void, Quantity = 5});
            CreateView();
            var ticket = (from t in m_view.Tickets where t.Comments == "Comments" select t).FirstOrDefault();
            var item = (from i in ticket.Items select i).FirstOrDefault();
            Assert.AreEqual("Description", item.Description);
            Assert.AreEqual("Yes", item.IsVoid);
            Assert.AreEqual("0", item.ReconciledQty);
            Assert.AreEqual("5", item.Quantity);
        }

        [Test]
        public void ShowsPours()
        {
            CreatePour(new DateTime(2011, 1, 30, 8, 0, 0), 10, "UpcName", "UpcCategory");
            CreatePour(new DateTime(2011, 1, 30, 8, 0, 0), 10, "UpcName", "UpcCategory");
            CreateView();
            Assert.AreEqual(2, m_view.Pours.ToList().Count);
        }

        [Test]
        public void ShowsPourFields()
        {
            CreatePour(new DateTime(2011, 1, 30, 8, 0, 0), 10, "UpcName", "UpcCategory");
            CreateView();
            var pour = (from p in m_view.Pours select p).FirstOrDefault();
            Assert.IsNotNull(pour);
            Assert.AreEqual(PosStatus.Pending, pour.Status);
            Assert.AreEqual(10, pour.PourAmount);
            Assert.AreEqual("UpcName", pour.Type);// type (UPC Name)
            Assert.AreEqual("UpcCategory", pour.Category); //, maybe Category (from UPC)
            Assert.AreEqual(new DateTime(2011, 1, 30, 8, 0, 0), pour.PourTime); //[9:38 AM] Bret Cline: and time
        }

        [Test]
        public void FiltersPoursByDate()
        {
            ((FakeClock)Services.Clock).Now = new DateTime(2011, 1, 31);
            CreatePour(new DateTime(2011, 1, 20, 8, 0, 0));
            CreatePour(new DateTime(2011, 1, 30, 8, 0, 0));
            CreateView();
            m_view.ViewPast = new TimeSpan(10, 0, 0, 0);
            m_view.FireRefreshClick();
            Assert.AreEqual(1, m_view.Pours.ToList().Count);
        }

        private void CreatePour(DateTime _pourTime, double _volume = 0.0, string _upcName = null, string _categoryName = null)
        {
            var category = m_factory.ManageCategories().Create();
            category.Name = _categoryName;
            m_factory.ManageCategories().Save(category);

            var upc = m_factory.ManageUPCs().Create();
            upc.CategoryID = category.CategoryID;
            upc.Name = _upcName;
            m_factory.ManageUPCs().Save(upc);

            var pour = m_factory.ManagePours().Create();
            pour.PourTime = _pourTime;
            pour.UPCID = upc.UPCID;
            pour.Volume = _volume;
            m_factory.ManagePours().Save(pour);
        }

        private void DeleteTickets()
        {
            var tickets = m_factory.ManagePOSTickets().GetAll();
            foreach (var ticket in tickets)
            {
                m_factory.ManagePOSTickets().Delete(ticket);
            }
        }
    }
}
