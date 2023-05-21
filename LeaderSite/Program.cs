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
    internal class Program : Driver
    {
        static void Main(string[] args)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.leader.ir/");
                Console.WriteLine("opened browser");


                var db = new Database();
                db.CreateDb();


                var archive = driver.FindElement(By.XPath("/html/body/footer/nav[3]/ul/li[5]/h6/a"));
                archive.Click();
                var dataList = driver.FindElements(By.XPath("/html/body/main/div[1]/section[1]/main/div[1]/ul/li"));
                int countDataListTopics = dataList.Count();
                List<string> listOfTopics = new List<string>();

                for (int i = 1; i <= countDataListTopics; i++)
                {
                    var topics = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[1]/ul/li[{i}]")).Text;
                    Console.WriteLine(topics);
                    listOfTopics.Add(topics);


                    if (topics == "بیانات")
                    {
                        var selectTopics = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[1]/ul/li[{i}]"));
                        Thread.Sleep(1000);
                        selectTopics.Click();
                    }
                }

                var years = driver.FindElements(By.XPath("/html/body/main/div[1]/section[1]/main/div[2]/ul/li"));
                var month = driver.FindElements(By.XPath("/html/body/main/div[1]/section[1]/main/div[3]/ul/li"));

                for (int i = 2; i <= 2; i++)
                {
                    var selectedYear = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[2]/ul/li[{i}]"));
                    Thread.Sleep(1000);
                    selectedYear.Click();

                    for (int j = 1; j <= month.Count; j++)
                    {
                        Thread.Sleep(1000);
                        var selectedMonth = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[3]/ul/li[{j}]"));
                        Thread.Sleep(1000);
                        selectedMonth.Click();
                        Thread.Sleep(1000);

                        var statements = driver.FindElements(By.XPath("/html/body/main/div[1]/section[2]/main/ul"));
                        Thread.Sleep(1000);

                        if (statements.Count == 0)
                        {
                            driver.Navigate().Refresh();
                        }

                        for (int k = 1; k <= statements.Count; k++)
                        {
                            Thread.Sleep(1000);

                            var selectedStatements = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[2]/main/ul[{k}]/li/div[2]/h6/a[2]"));
                            selectedStatements.Click();

                            var newsTime = driver.FindElement(By.XPath("/html/body/main/div[1]/section/main/article[1]/div[1]/time/h6")).Text;
                            newsTime = new string(newsTime.Where(c => !char.IsPunctuation(c)).ToArray());

                            Thread.Sleep(1000);

                            db.SaveDb();

                            driver.Navigate().Back();


                            var selectTopics = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[1]/ul/li[{i}]"));
                            Thread.Sleep(1000);
                            selectTopics.Click();
                            Thread.Sleep(500);

                            var selectedDefultYears = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[2]/ul/li[{i}]"));
                            Thread.Sleep(500);

                            selectedDefultYears.Click();
                            Thread.Sleep(500);
                            var selectedDefultMonth = driver.FindElement(By.XPath($"/html/body/main/div[1]/section[1]/main/div[3]/ul/li[{j}]"));

                            selectedDefultMonth.Click();
                            Thread.Sleep(500);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //driver.Quit();
        }
    }
}
