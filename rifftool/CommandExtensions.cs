using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;

namespace rifftool 
{
    public static class CommandExtensions
    {
        /// <summary>
        /// Add the options to the <cref see="Command"/>
        /// </summary>
        /// <param name="command">Command to add to</param>
        /// <param name="options">Options to add</param>
        public static void AddOptions(this Command command, params Option[] options)
        {
            foreach (var opt in options)
            {
                command.AddOption(opt);
            }
        }
        
        /// <summary>
        /// Add the options; 0 or 1 can be applied.
        /// </summary>
        /// <param name="command">Command to add to</param>
        /// <param name="options">Options to add</param>
        public static void AddMutuallyExclusive(this Command command, params Option[] options)
        {
            AddOptions(command, options);
            command.AddValidator(result => 
            {
                return ValidateMutualExclusion(result, options);
            });
        }

        /// <summary>
        /// Add the options; only 1 can be applied.
        /// </summary>
        /// <param name="command">Command to add to</param>
        /// <param name="options">Options to add</param>
        public static void AddMutuallyExclusiveRequired(this Command command, params Option[] options)
        {
            AddOptions(command, options);
            command.AddValidator(result => 
            {               
                return ValidateMutualExclusion(result, options) ?? ValidateOnlyOne(result, options);
            });
        }

        private static string ValidateMutualExclusion(CommandResult result, Option[] options)
        {
            if (options.Count(o => result.ValueForOption(o.Name)!=null)>1)
            {
                return "The options [" + ToString(options) + "] are mutually exclusive.";
            }
            
            return null;
        }

        private static string ValidateOnlyOne(CommandResult result, Option[] options)
        {
            if (options.Count(o => result.ValueForOption(o.Name)!=null)!=1)
            {
                return "One of the options [" + ToString(options) + "] is required.";
            }
            
            return null;
        }

        private static string ToString(Option[] options)
        {
            return String.Join(", ", options.AsEnumerable<Option>().Select(o => o.RawAliases.First()));
        }
    }
}