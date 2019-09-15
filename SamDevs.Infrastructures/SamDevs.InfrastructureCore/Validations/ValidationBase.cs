using System.Collections.Generic;
using System.Linq;

namespace SamDevs.InfrastructureCore.Validations {
    public abstract class ValidationBase {
        

        protected ValidationBase() {
            
            BrokenRules = new List<ISpecification>();
        }
        public IList<ISpecification> BrokenRules { get; }

        public IList<string> ValidationMessages() {
            return BrokenRules.Select(val => val.Message).ToList();
        }
        public string ValidationMessage() {
            return ValidationMessages().Aggregate((a, b) => a + ", " + b);
        }


        protected abstract void Validate();

        public bool IsValid() {
            BrokenRules.Clear();
            Validate();
            return BrokenRules.Count == 0;
        }

        protected void AddBrokenRules(ISpecification brokenRule) {
            BrokenRules.Add(brokenRule);
        }


    }
}
