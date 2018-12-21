using System.Collections.Generic;
using System.IO;

namespace PatchworkLauncher.Comparers
{
	public class PatchFileNameEqualityComparer : IEqualityComparer<XmlInstruction>
	{
		#region Public Methods and Operators

		public bool Equals(XmlInstruction x, XmlInstruction y)
		{
			return Path.GetFileName(x?.Location) == Path.GetFileName(y?.Location);
		}

		public int GetHashCode(XmlInstruction obj)
		{
			return Path.GetFileName(obj.Location).GetHashCode();
		}

		#endregion
	}
}
