using System;
using System.ComponentModel;
using System.Resources;

namespace SamDevs.Infrastructure.Attributes {
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DisplayNameForClassAttribute : DisplayNameAttribute {
        public Type ResourceType { get; set; }

        public DisplayNameForClassAttribute(string displayName):base(displayName) {
            
        }
        public override string DisplayName {
            get {
                if (ResourceType == null) return DisplayName;
                var resourceManager = new ResourceManager(ResourceType);
                return resourceManager.GetString(DisplayNameValue);
            }
        }
    }
}
