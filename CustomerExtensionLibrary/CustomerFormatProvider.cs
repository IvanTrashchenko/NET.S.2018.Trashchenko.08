using System;
using System.Globalization;
using System.Text;
using CustomerLibrary;

namespace CustomerExtensionLibrary
{
    /// <summary>
    /// Additional format provider for <see cref="Customer"/> class.
    /// </summary>
    public class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
        #region Fields

        /// <summary>
        /// Parent format provider.
        /// Default: <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        private readonly IFormatProvider parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerFormatProvider"/> class with default values.
        /// </summary>
        public CustomerFormatProvider()
            : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerFormatProvider"/> class.
        /// </summary>
        /// <param name="parent">Specific <see cref="IFormatProvider"/> parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown when <see cref="IFormatProvider"/> parameter is null.</exception>
        public CustomerFormatProvider(IFormatProvider parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException($"{nameof(parent)} can not be null.");
            }

            this.parent = parent;
        }

        #endregion

        #region IFormatProvider implementation

        /// <summary>
        /// Method for retrieving an object to control formatting.
        /// </summary>
        /// <param name="formatType">Type of format.</param>
        /// <returns>Instance of the object if <see cref="IFormatProvider"/> implementation is correct; otherwise null.</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        #endregion

        #region ICustomFormatter implementation

        /// <summary>
        /// Method which supports custom formatting for <see cref="Customer"/> class.
        /// 'F' for <see cref="Customer"/>'s first name;
        /// 'L' for <see cref="Customer"/>'s last name.
        /// </summary>
        /// <param name="format">Format specifier.</param>
        /// <param name="arg">Object to be formatted.</param>
        /// <param name="formatProvider">Format provider.</param>
        /// <returns>A string representation of this instance formatted according to it's specifier.</returns>
        /// <exception cref="FormatException">Thrown when format specifier or object to be formatted is incorrect.</exception>
        public string Format(string format, object arg, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
            {
                throw new FormatException($"Incorrect {nameof(format)} specifier.");
            }

            if (arg == null)
            {
                throw new FormatException($"{nameof(arg)} can not be null.");
            }

            if (arg.GetType() != typeof(Customer))
            {
                throw new FormatException($"{nameof(arg)} must be Customer type.");
            }

            if (formatProvider == null)
            {
                formatProvider = this.parent;
            }

            Customer customer = (Customer)arg;

            StringBuilder result = new StringBuilder("Customer record: ");

            for (int i = 0; i < format.Length; i++)
            {
                switch (format[i])
                {
                    case 'N':
                        result.Append(string.Format(formatProvider, $"{customer.Name}"));
                        break;
                    case 'F':
                        result.Append(string.Format(formatProvider, $"{customer.Name}").Split(' ')[0]);
                        break;
                    case 'L':
                        result.Append(string.Format(formatProvider, $"{customer.Name}").Split(' ')[1]);
                        break;
                    case 'P':
                        result.Append(string.Format(formatProvider, $"{customer.ContactPhone}"));
                        break;
                    case 'R':
                        result.Append(customer.Revenue.ToString("N02", formatProvider));
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

        #endregion
    }
}