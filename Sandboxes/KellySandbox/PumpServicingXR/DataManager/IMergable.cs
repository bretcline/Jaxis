using System;
using System.Runtime.Serialization;

namespace LFI.Sync.DataManager
{
	public interface IMergable<O>
	{
		bool DataEquals(O inObj);
		void Merge(O inObj);
	}
}
