using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntroToMVC.UI.MVC.Models;

namespace IntroToMVC.UI.MVC.Controllers
{
    public class ViewBagDataController : Controller
    {
        // GET: ViewBagData
        public ActionResult Index()
        {
            //The Viewbag is a dynamic variable which means it does not always exist until runtime. When I attempt to call this variable from the UI(View) we will not get intellisense.
            //The Viewbag can be used to pass data between an action and it's corresponding view. Be aware you cannot pass ViewBag objects between actions or different views.

            ViewBag.Action = "Hello from the action.";

            //Any declarations of variables or calculations or any other activity needs to be before the return VIEW();

            return View();
        }

        //Parameter variables must match the input Name attribute in the UI. Spelling counts, casing does not matter.
        public ActionResult Greeting(string firstName, string lastName)
        {
            //write the logic for the initial request in the default of our ViewBag variable if the string(s) are empty or null.
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                ViewBag.Greeting = null;
            } else
            {
                ViewBag.Greeting = $"Welcome {firstName} {lastName}!";
            }
            return View();
        }

        //When this action first gets hit both values will be null. At the moment the double datatype is not allowed to be null. We need to add the ? after the datatype to allow these variables to be nullable
        public ActionResult SimpleCalculator(double? number1, double? number2, string function)
        {
            if(number1 != null && number2 != null)
            {
                switch (function)
                {
                    case "Add":
                        ViewBag.Answer = $"The result of your calculation is {(number1 + number2)}";
                        break;
                    case "Subtract":
                        ViewBag.Answer = $"The result of your calculation is {(number1 - number2)}";
                        break;
                    case "Multiply":
                        ViewBag.Answer = $"The result of your calculation is {(number1 * number2)}";
                        break;
                    default:
                        if(number2 != 0)
                        {
                            ViewBag.Answer = $"The result of your calculation is {(number1 / number2)}";
                        } else
                        {
                            ViewBag.Answer = "Cannot divide by zero.";
                        }
                        break;
                }
            } else
            {
                ViewBag.Answer = "This calculator requires 2 valid values.";
            }
            return View();
        }

        public ActionResult Module6MadLib(string foreignCountry, string adjective, string animal, string verb, string place, string typeOfLiquid)
        {
            if(!string.IsNullOrEmpty(foreignCountry) && 
                !string.IsNullOrEmpty(adjective) && 
                !string.IsNullOrEmpty(animal) && 
                !string.IsNullOrEmpty(verb) && 
                !string.IsNullOrEmpty(place) && 
                !string.IsNullOrEmpty(typeOfLiquid))
            {
                ViewBag.MadLib = $"If you are traveling in {foreignCountry} and find yourself having to cross a piranha filled river, here's how to do it safely. Piranahas are more {adjective} during the day, so cross the river at night. Avoid areas with netted {animal} traps - piranhas may be waiting there looking to {verb} them. Piranhas are attraced to fresh {typeOfLiquid} and will migrate to it as often as possible. Whatever you do, if you have an open wound, try to find another way to get back to the {place}.";
            } else
            {
                ViewBag.MadLib = "Please complete all fields.";
            }

            return View();
        }

        public ActionResult Module6WaterLab(double? waterInput)
        {
            if (waterInput != null)
            {
                ViewBag.WaterAnswer = $"That many gallons would weight {waterInput * 8.33} pounds.";
            } else
            {
                ViewBag.WaterAnswer = "Please enter an amount of water in gallons.";
            }
            return View();
        }

        public ActionResult Module6ChangeLab(decimal? moneyInput)
        {
            if(moneyInput != null)
            //if (moneyInput > 0)
            {
                decimal initialInput = moneyInput ?? 0;
                decimal totalQuarters = Math.Floor(initialInput / .25m);

                initialInput = initialInput % .25m;
                decimal totalDimes = Math.Floor(initialInput / .1m);

                initialInput = initialInput % .1m;
                decimal totalNickels = Math.Floor(initialInput / .05m);

                initialInput = initialInput % .05m;
                decimal totalPennys = Math.Floor(initialInput / .01m);

                ViewBag.ChangeAnswer = $"Quarters: {totalQuarters} Dimes: {totalDimes} Nickels: {totalNickels} Pennys: {totalPennys}";
            } else
            {
                ViewBag.ChangeAnswer = "please enter a value.";
            }
            return View();
        }

        public ActionResult Module6MaxMinAverage(double? one, double? two, double? three, double? four, double? five)
        {
            if (one != null && two != null && three != null && four != null && five != null)
            {
                double?[] averageArray = { one, two, three, four, five };
                Array.Sort(averageArray);
                double? average = averageArray.Sum() / (averageArray.Length);

                ViewBag.AverageAnswer = $"The max number was {averageArray[4]}. The min number was {averageArray[0]}. The average was {average}.";

            } else
            {
                ViewBag.AverageAnswer = "please submit 5 values.";
            }
            
            return View();
        }

        public ActionResult Module6TempConverter(double? tempInput)
        {
            if (tempInput != null)
            {
                ViewBag.Temp = $"{tempInput} degrees C is equal to {tempInput * 9 / 5 + 32} degrees F.";
            }else
            {
                ViewBag.Temp = "Please enter a temperature to convert.";
            }
            return View();
        }

        public ActionResult Module6InterestCalc(double? interestBalanceInput, double? interestRateInput, double? interestYears)
        {
            if (interestBalanceInput != null && interestRateInput != null && interestYears != null)
            {
                double? answer = interestBalanceInput;
                double? whileLoopVar = 0;
                while (whileLoopVar < interestYears)
                {
                    answer = answer + (answer * (interestRateInput / 100));
                    whileLoopVar++;
                }
                ViewBag.InterestAnswer = $"You would have {answer} after {interestYears} years.";
            } else
            {
                ViewBag.InterestAnswer = "Please enter the above information";
            }
            return View();
        }


        ///////////////////////Put labs above////////////////////////
        
        public ActionResult ShowFamilyMember()
        {
            //When the user selects this view, create a FamilyMemberViewModel object and return it to the view for display
            FamilyMemberViewModel fm = new FamilyMemberViewModel(111, "Scott", "Father", new DateTime(1952, 12, 26));

            ViewBag.Family = fm;

            return View();
        }
    }
}