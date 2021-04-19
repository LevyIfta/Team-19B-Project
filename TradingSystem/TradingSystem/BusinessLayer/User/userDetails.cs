using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class userDetails
    {
        public Dictionary<string, LinkedList<iDecorator>> ManagerPremistion;
        public Dictionary<string, LinkedList<string>> hireManager;
        public userDetails()
        {
            ManagerPremistion = new Dictionary<string, LinkedList<iDecorator>>();
            hireManager = new Dictionary<string, LinkedList<string>>();
        }

        public bool haveManagerPremisition(string storeName){ return (ManagerPremistion[storeName].Count != 0);}

        public iDecorator doManage(string storeName, string functionName){
            foreach (iDecorator temp in ManagerPremistion[storeName]){
                if(temp.functionName().Equals(functionName))
                    return temp;
            }
            return null;
        }
        
        public void addPremission(string storeName, List<string> pre){
            foreach (string temp in pre){
                ManagerPremistion[storeName].AddLast(Decorator.createPremisstion(temp));
            }
        }
        public void editPremission(string storeName, List<string> pre){
            LinkedList<iDecorator> newPre = new LinkedList<iDecorator>();
            foreach (string temp in pre){
                newPre.AddLast(Decorator.createPremisstion(temp));
            }
            ManagerPremistion[storeName] = newPre;
        }
        public void removePremission(string storeName){
            ManagerPremistion.Remove(storeName);
        }
    }
}
