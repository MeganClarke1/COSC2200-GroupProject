/// <file>
///   <summary>
///     File Name: CommandBase.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 8, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This file contains the base Command class to be inherited by other command classes.
///   </description>
/// </file>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Group2_COSC2200_Project.commands
{
    /// <summary>
    /// An abstract base command class to be inherited by all other command classes.
    /// </summary>
    public abstract class CommandBase : ICommand
    {   
        /// <summary>
        /// The event handler for executing a change.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Can Execute is the first portion of the required command entities. By default this will be set to true.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// An abstract execution method that will be overridden by inheriting command classes to exectute their specific
        /// commands.
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// The event to be monitor so that changes will be sent/notified to the window.
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
