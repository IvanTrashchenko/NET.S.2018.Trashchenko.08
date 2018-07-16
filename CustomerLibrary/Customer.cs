using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CustomerLibrary
{
    /// <summary>
    /// Customer class.
    /// </summary>
    public class Customer : IFormattable
    {
        #region Fields

        /// <summary>
        /// Name of customer.
        /// </summary>
        private string name;

        /// <summary>
        /// Contact phone of customer.
        /// </summary>
        private string contactPhone;

        /// <summary>
        /// Customer's revenue.
        /// </summary>
        private decimal revenue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class with default values.
        /// </summary>
        public Customer()
        {
            this.Name = string.Empty;
            this.ContactPhone = string.Empty;
            this.Revenue = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="name">Name of customer.</param>
        /// <param name="contactPhone">Contact phone of customer.</param>
        /// <param name="revenue">Customer's revenue.</param>
        public Customer(string name, string contactPhone, decimal revenue)
        {
            this.Name = name;
            this.ContactPhone = contactPhone;
            this.Revenue = revenue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when name is incorrect.</exception>
        /// <exception cref="ArgumentNullException">Thrown when name value is null.</exception>
        public string Name
        {
            get => this.name;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"{nameof(value)} can not be null.");
                }

                Regex regex = new Regex(@"^([A-Z][a-z]*\s)*[A-Z][a-z]*$");

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException($"{nameof(value)} is incorrect.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets the contact phone.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when contact phone is incorrect.</exception>
        /// /// <exception cref="ArgumentNullException">Thrown when contact phone value is null.</exception>
        public string ContactPhone
        {
            get => this.contactPhone;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"{nameof(value)} can not be null.");
                }

                Regex regex = new Regex(@"^\+\d+\s(\(\d+\))\s\d{3}-\d{4}$");

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException($"{nameof(value)} is incorrect.");
                }

                this.contactPhone = value;
            }
        }

        /// <summary>
        /// Gets the revenue.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when revenue's value is out of range.</exception>
        public decimal Revenue
        {
            get => this.revenue;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} is out of range.");
                }

                this.revenue = value;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts this instance to it's <see cref="string"/> representation with specific format provider.
        /// 'N' for <see cref="Customer"/>'s full name;
        /// 'P' for <see cref="Customer"/>'s contact phone;
        /// 'R' for <see cref="Customer"/>'s revenue value.
        /// </summary>
        /// <param name="format">Format specifier.</param>
        /// <param name="formatProvider">Format provider.</param>
        /// <returns>A string representation of this instance.</returns>
        /// <exception cref="FormatException">Thrown when format specifier is incorrect.</exception>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
            {
                throw new FormatException($"Incorrect {nameof(format)} specifier.");
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            StringBuilder result = new StringBuilder("Customer record: ");

            for (int i = 0; i < format.Length; i++)
            {
                switch (format[i])
                {
                    case 'N':
                        result.Append(string.Format(formatProvider, $"{this.Name}"));
                        break;
                    case 'P':
                        result.Append(string.Format(formatProvider, $"{this.ContactPhone}"));
                        break;
                    case 'R':
                        result.Append(this.Revenue.ToString("N02", formatProvider));
                        break;
                    default:
                        throw new FormatException($"Incorrect {nameof(format)} specifier.");
                }

                if (i != format.Length - 1)
                {
                    result.Append(", ");
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts this instance to it's <see cref="string"/> representation.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString() => this.ToString("NRP");

        #endregion

    }
}