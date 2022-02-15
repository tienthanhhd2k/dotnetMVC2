using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetcoremvc2.Models;

namespace dotnetcoremvc2.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(ILogger<PeopleController> logger)
    {
        _logger = logger;
    }

    public static List<PersonModel> people = new List<PersonModel>{ 
            new PersonModel{
                Id = 1,
                FirstName = "Do",
                LastName = "Thanh",
                Address = "Hai Duong",
                Gender = "Male"
            },

            new PersonModel{
                Id = 2,
                FirstName = "Nguyen",
                LastName = "Binh",
                Address = "Ha Noi",
                Gender = "Male"
            },

            new PersonModel{
                Id = 3,
                FirstName = "Tran",
                LastName = "Nam",
                Address = "Hai Phong",
                Gender = "Male"
            }
        };
    public IActionResult Index()
    {
        

        return View(people);
    }

    public IActionResult Edit(int id )
    {
        var person = people.Where(x=>x.Id==id).FirstOrDefault();
        return View(person);
    }
    [HttpPost]
    public IActionResult Edit(PersonModel person)
    {
        var EditPerson = (from person1 in people
        where person1.Id == person.Id
        select person1).FirstOrDefault();
        EditPerson.FirstName = person.FirstName;
        EditPerson.LastName = person.LastName;
        EditPerson.Address = person.Address;
        EditPerson.Gender = person.Gender;
        return RedirectToAction("Index");
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(PersonModel model)
    {
        // Add new person logics
        people.Add(model);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(PersonModel person)
    {   var DeletePerson = (from person2 in people
        where person2.Id == person.Id
        select person2).FirstOrDefault();
        people.Remove(DeletePerson);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
