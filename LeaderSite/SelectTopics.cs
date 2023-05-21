using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using System.Data.SQLite;

namespace LeaderSite
{
    internal class SelectTopics : Driver
    {
        public void topicSelected()
        {
            var archive = driver.FindElement(By.XPath("/html/body/footer/nav[3]/ul/li[5]/h6/a"));
            archive.Click();
            var dataList = driver.FindElements(By.XPath("/html/body/main/div[1]/section[1]/main/div[1]/ul/li"));
            int countDataListTopics = dataList.Count();
            List<string> listOfTopics = new List<string>();

            for (int t = 1; t <= countDataListTopics; t++)
            {
                var topics = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[1]/ul/li[{t}]")).Text;
                Console.WriteLine(topics);
                listOfTopics.Add(topics);


                if (topics == "بیانات")
                {
                    var selectTopics = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[1]/ul/li[{t}]"));
                    Thread.Sleep(1000);
                    selectTopics.Click();
                }
            }
        }
    }
}
