using System;
using System.Globalization;

namespace LogicBuilder.RulesDirector
{
    [Serializable]
    public sealed class ProgressInfo(string description) : IEquatable<ProgressInfo>, IComparable<ProgressInfo>
    {

        #region Variables
        private readonly string description = description;
        private readonly DateTime dateAndTime = DateTime.UtcNow;
        #endregion Variables

        #region Properties
        public string Description
        {
            get { return description; }
        }

        public DateTime DateAndTime
        {
            get { return dateAndTime; }
        }
        #endregion Properties

        #region IEquatable<ProgressInfo> Members
        public bool Equals(ProgressInfo other)
        {
            if (other == null)
                return false;

            return this.description.Equals(other.description);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProgressInfo);
        }
        #endregion

        #region IComparable<ProgressInfo> Members
        public int CompareTo(ProgressInfo other)
        {
            if (other == null)
                return 1;

            return this.description.CompareTo(other.description);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, Strings.variableTypeToStringFormat, this.description, this.dateAndTime.ToString("T", CultureInfo.CurrentCulture));
        }

        public override int GetHashCode()
        {
            return description.GetHashCode();
        }

        public static bool operator ==(ProgressInfo left, ProgressInfo right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(ProgressInfo left, ProgressInfo right)
        {
            return !(left == right);
        }

        public static bool operator <(ProgressInfo left, ProgressInfo right)
        {
            if (left is null)
                return right is not null;
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ProgressInfo left, ProgressInfo right)
        {
            if (left is null)
                return true;
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ProgressInfo left, ProgressInfo right)
        {
            if (left is null)
                return false;
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ProgressInfo left, ProgressInfo right)
        {
            if (left is null)
                return right is null;
            return left.CompareTo(right) >= 0;
        }
        #endregion Methods
    }
}
