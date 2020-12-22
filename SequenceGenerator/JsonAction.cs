using Castle.DynamicProxy.Generators.Emitters.CodeBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    public interface IJsonAction
    {
        /// <summary>
        /// Gets the type of action this is.
        /// </summary>
        /// <returns>the type of action.</returns>
        string ActionType();

        /// <summary>
        /// Gets the next action in the sequence.
        /// </summary>
        /// <returns></returns>
        string GetNextAction();

        /// <summary>
        /// Applies the result to the specified creation.
        /// </summary>
        /// <param name="creation">The creation to be applied to.</param>
        void ApplyResult(ref Creation creation);

        /// <summary>
        /// Validates the action based off it's data.
        /// </summary>
        /// <returns></returns>
        bool Validate();

    }


    public class JsonAction : IJsonAction
    {
        public string NextAction;

        public virtual string ActionType() { return null; }
        public virtual void ApplyResult(ref Creation creation) { }

        public string GetNextAction()
        {
            return NextAction;
        }

        public virtual bool Validate() { return true; }

        /// <summary>
        /// Creates a dummy action.
        /// </summary>
        /// <returns>a dummy action based off the current class.</returns>
        public virtual object CreateDummyAction() { return null; }


    }
}
