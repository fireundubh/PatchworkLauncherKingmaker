using System.Collections.Generic;

namespace PatchworkLauncher.Comparers
{
	public class PatchLocationEqualityComparer : IEqualityComparer<XmlInstruction>
	{
		#region Public Methods and Operators

		public bool Equals(XmlInstruction x, XmlInstruction y)
		{
			return x?.Location == y?.Location;
		}

		public int GetHashCode(XmlInstruction obj)
		{
			return obj.Location.GetHashCode();
		}

		#endregion
	}
}
