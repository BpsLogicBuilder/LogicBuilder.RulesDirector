using System;
using System.Globalization;

namespace LogicBuilder.RulesDirector
{
    [Serializable]
    public class ProgressInfo(string description) : IEquatable<ProgressInfo>, IComparable<ProgressInfo>
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
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.invalidArgumentFormat, "{9F3F5272-92FC-4d9d-A9E3-6D5F313308A1}"));

            return this.description.Equals(other.description);
        }
        #endregion

        #region IComparable<ProgressInfo> Members
        public int CompareTo(ProgressInfo other)
        {
            if (other == null)
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.invalidArgumentFormat, "{F03ADAE7-1CD3-4992-A9C8-221D275E85CA}"));

            return this.description.CompareTo(other.description);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, Strings.variableTypeToStringFormat, this.description, this.dateAndTime.ToString("T", CultureInfo.CurrentCulture));
        }
        #endregion Methods
    }
}
