using System;
using System.Collections;
using System.Reflection;

namespace WebApp.Models
{
    public class PageMessageModel
    {

        public MessageType Type { get; set; }
        public string Message { get; set; }

        //An enum with all types of message and their afferent CSS classes
        public enum MessageType
        {
            [EnumString("alert alert-success")]
            Success = 0,
            [EnumString("alert alert-info")]
            Info = 1,
            [EnumString("alert alert-error")]
            Error = 2,
            [EnumString("alert")]
            Warning = 3
        }

        /// <summary>
        /// Attribute class that expose a string through a Value property.
        /// </summary>
        public class EnumStringAttribute : Attribute
        {
            private string value;

            public EnumStringAttribute(string value)
            {
                this.value = value;
            }

            public string Value
            {
                get { return this.value; }
            }
        }

        /// <summary>
        /// Returns the EnumString attribute asociated to an Enum Value if it exists.
        /// </summary>
        /// <param name="en">Enum Value</param>
        /// <returns>The String value asociated with it.</returns>
        public static string GetEnumString(Enum en)
        {
            Hashtable strValues = new Hashtable();
            string str = null;
            Type type = en.GetType();

            if (strValues.ContainsKey(en))
                str = (strValues[en] as EnumStringAttribute).Value;
            else
            {
                FieldInfo fi = type.GetField(en.ToString());
                EnumStringAttribute[] attrs =
                   fi.GetCustomAttributes(typeof(EnumStringAttribute), false) as EnumStringAttribute[];
                if (attrs.Length > 0)
                {
                    strValues.Add(en, attrs[0]);
                    str = attrs[0].Value;
                }
            }
            return str;
        }
    }
}