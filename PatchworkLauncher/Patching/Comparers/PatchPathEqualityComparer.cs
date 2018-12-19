using System.Collections.Generic;

namespace PatchworkLauncher.Comparers
{
	public class PatchPathEqualityComparer : IEqualityComparer<XmlInstruction>
	{
		public bool Equals(XmlInstruction x, XmlInstruction y)
		{
			return x.PatchLocation == y.PatchLocation;
		}

		public int GetHashCode(XmlInstruction obj)
		{
			return obj.PatchLocation.GetHashCode();
		}
	}
}
