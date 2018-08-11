using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    /// <summary>
    /// Helper class in making dynamic class definitions easier.
    /// </summary>
    public sealed class BaseClassMaker
    {
        private string _typeName;
        private StringBuilder _interfaces;

        public BaseClassMaker(string baseClassName = null)
        {
            SetBaseClassName(baseClassName);
        }

        /// <summary>
        /// Sets the base-class name.
        /// </summary>
        public void SetBaseClassName(string typeName)
        {
            _typeName = typeName;
        }

        /// <summary>
        /// Appends additional implemented interface.
        /// </summary>
        public bool AddInterface(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return false;

            if (_interfaces == null)
            {
                _interfaces = new StringBuilder();
            }
            else
            {
                if (_interfaces.Length > 0)
                {
                    _interfaces.Append(", ");
                }
            }

            _interfaces.Append(typeName);
            return true;
        }

        /// <summary>
        /// Conditionally appends additional implemented interface.
        /// </summary>
        public bool AddInterface(string interfaceName, bool condition)
        {
            if (condition)
            {
                return AddInterface(interfaceName);
            }

            return false;
        }

        public override string ToString()
        {
            var hasInterfaces = _interfaces != null && _interfaces.Length > 0;

            if (string.IsNullOrEmpty(_typeName))
            {
                return hasInterfaces ? " : " + _interfaces : string.Empty;
            }

            return hasInterfaces ? string.Concat(" : ", _typeName, ", ", _interfaces) : " : " + _typeName;
        }
    }
}
