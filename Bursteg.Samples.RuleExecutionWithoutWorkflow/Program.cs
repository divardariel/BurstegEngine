//=====================================================================
// Rules Execution without Workflow Sample
//
// Guy Burstein
// http://blogs.microsoft.co.il/blogs/bursteg
//
// October 11th, 2006
//
//=====================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Activities.Rules.Design;
using System.Windows.Forms;
using System.Reflection;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.IO;
using System.Xml;

namespace Bursteg.Samples.RuleExecutionWithoutWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ruleset to work with
            RuleSet ruleSet = null;

            // Rules file name to serialize to and deserialize from
            string fileName = "Bursteg.Samples.RuleExecution.rules";

            // Define a RuleSet or use an existing one
            DialogResult createDialogResult = MessageBox.Show("Do you want to create a new RuleSet?", "Workflow Rules", MessageBoxButtons.YesNo);

            // If yes, open the RuleSetDialog
            if (createDialogResult == DialogResult.Yes)
            {
                // Create a RuleSet that waorks with Orders (just another .net Object)
                RuleSetDialog ruleSetDialog = new RuleSetDialog(typeof(Order), null, ruleSet);

                // Show the RuleSet Editor
                DialogResult result = ruleSetDialog.ShowDialog();

                // Get the RuleSet after editing
                ruleSet = ruleSetDialog.RuleSet;

                if (result == DialogResult.OK)
                {
                    // Serialize to a .rules file
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    
                    XmlWriter rulesWriter = XmlWriter.Create(fileName);
                    serializer.Serialize(rulesWriter, ruleSet);
                    rulesWriter.Close();
                }
            }
            else
            {
                // Deserialize from a .rules file.
                XmlTextReader rulesReader = new XmlTextReader(fileName);
                WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                ruleSet = (RuleSet)serializer.Deserialize(rulesReader);
                rulesReader.Close();
            }

            // Create an instance of the Business Entity, and print its properties
            // before executing the rules
            Order myOrder = new Order("Advantech", 3, 3.04M);
            Console.WriteLine("Before executing rules:");
            PrintProperties(myOrder);

            // Execute the rules and print the entity's properties
            RuleValidation validation = new RuleValidation(typeof(Order), null);
            RuleExecution execution = new RuleExecution(validation, myOrder);
            ruleSet.Execute(execution);

            Console.WriteLine("\nAfter executing rules:");
            PrintProperties(myOrder);
        }

        /// <summary>
        /// Prints the order properties and the total price
        /// </summary>
        /// <param name="myOrder"></param>
        private static void PrintProperties(Order myOrder)
        {
            Type orderType = myOrder.GetType();
            foreach ( PropertyInfo pi in orderType.GetProperties() )
            {
                Console.WriteLine("{0}= {1}", pi.Name, pi.GetValue(myOrder, new object[]{}));
            }
            Console.WriteLine("Total Price = " + myOrder.GetTotalPrice().ToString());
        }
    }
}
