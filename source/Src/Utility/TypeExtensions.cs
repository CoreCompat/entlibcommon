// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Utility
{
    /// <summary>
    /// Extensios to <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        ///<summary>
        /// Locates the generic parent of the type
        ///</summary>
        ///<param name="rootType">Type to begin search from.</param>
        ///<param name="parentType">Open generic type to seek</param>
        ///<returns>The found parent that is a closed generic of the <paramref name="parentType"/> or null</returns>
        public static Type FindGenericParent(this Type rootType, Type parentType)
        {
            if (parentType == null) throw new ArgumentNullException("parentType");
            if (rootType == null) throw new ArgumentNullException("rootType");

            var parentTypeInfo = parentType.GetTypeInfo();
            var rootTypeInfo = rootType.GetTypeInfo();

            if (!parentTypeInfo.IsGenericType) return null;

            var currentTypeInfo = rootTypeInfo;

            while (currentTypeInfo.UnderlyingSystemType != typeof(object))
            {
                if (!currentTypeInfo.IsGenericType)
                {
                    currentTypeInfo = currentTypeInfo.BaseType.GetTypeInfo();
                    continue;
                }

                var genericType = currentTypeInfo.GetGenericTypeDefinition();
                if (genericType == parentType) return currentTypeInfo.UnderlyingSystemType;

                currentTypeInfo = currentTypeInfo.BaseType.GetTypeInfo();
            }

            return null;
        }
    }
}
