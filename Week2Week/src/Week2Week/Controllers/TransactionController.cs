using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Week2Week.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Week2Week.Models.TransactionViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Week2Week.Models;
using Week2Week.Data;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Week2Week.Controllers
{         [Authorize]
    public class TransactionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        //Sets private property of BangazonContext;
        private ApplicationDbContext context;

        //Method: creates custom constructor method with argument of context, therefore rendering context public 
        public TransactionController(ApplicationDbContext ctx, UserManager<ApplicationUser> user)
        {
            _userManager = user;
            context = ctx;
        }

        //Method: creates async method for two purposes: extract the Customer table from current context for extraction into the dropdown menu and return the Index view of complete Transaction list
        public async Task<IActionResult> Index()
        {
            // Create new instance of the view model
            TransactionList model = new TransactionList(context);

            // Set the properties of the view model
            model.Transactions = await context.Transaction.OrderBy(s => s.Title.ToUpper()).ToListAsync();

            return View(model);
        }

        //Method: purpose is to return the AllTransactionsView only show Transactions in the selected filtered by subcategory. Accepts an argument of the selected subcategory's id
        public async Task<IActionResult> TransactionsInSubCategory([FromRoute] int id)
        {
            TransactionList model = new TransactionList(context);

            model.Transactions = await context.Transaction.Where(p => p.TransactionSubTypeId == id).OrderBy(s => s.Title.ToUpper()).ToListAsync();
            // codebase.Methods.Where(x => (x.Body.Scopes.Count > 5) && (x.Foo == "test"));
            return View("Index", model);

        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        //Method: purpose is to create Transactions/Create view that delivers the form to create a new Transaction, including the Transaction type dropdown (will need adjustment when creating subcategories) and customer dropdown on navbar

        [HttpGet]
        public IActionResult Create()
        {
            CreateTransaction model = new CreateTransaction(context);
            return View(model);
        }

        //Method: Purpose is to send the customer's Transaction to the database and then redirects the user to the homepage (AllTransactionsView)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Transaction transaction)
        {
            //Ignore user from model state
            ModelState.Remove("transaction.User");

            //This creates a new variable to hold our current instance of the ActiveCustomer class and then sets the active customer's id to the CustomerId property on the Transaction being created so that a valid model is sent to the database

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                transaction.User = user;
                context.Add(transaction);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //This creates a new instance of the CreateTransactionViewModel so that we can return the same view (i.e., the existing Transaction info user has entered into the form) if the model state is invalid when user tries to create Transaction
            CreateTransaction model = new CreateTransaction(context);
            return View(model);
        }

        [HttpPost]
        public IActionResult GetSubTypes([FromRoute]int id)
        {
            //get sub categories with that Transaction type on them
            var subTypes = context.TransactionSubType.OrderBy(s => s.SubType.ToUpper()).Where(t => t.TransactionTypeId == id).ToList();
            return Json(subTypes);
        }

        //Method: Purpose is to route user to the detail view on a selected Transaction. Accepts an argument (passed in through the route) of the Transaction's primary key (id)

        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            //throw a 404(NotFound) error if method is called w/o id in route
            if (id == null)
            {
                return NotFound();
            }

            // Create a new instance of the TransactionDetailViewModel and pass it the existing BangazonContext (current db session) as an argument in order to extract the Transaction whose id matches the argument passed in¸
            TransactionDetail model = new TransactionDetail(context);

            // Set the `Transaction` property of the view model and include the Transaction's seller (i.e., its .Customer property, accessed via Include, which traverses Transaction table and selects the Customer FK)
            model.Transaction = await context.Transaction
                    .Include(trans => trans.User)
                    .SingleOrDefaultAsync(trans => trans.TransactionId == id);

            // If no matching Transaction found, return 404 error
            if (model.Transaction == null)
            {
                return NotFound();
            }

            //Otherwise, return the TransactionDetailViewModel view with the TransactionDetailViewModel passed in as argument for rendering that specific Transaction on the page

            return View(model);
        }

        //Method: Purpose is to return a view that displays all the Transactions of one category. Accepts one argument, passed in through route, of TransactionTypeId.
        public async Task<IActionResult> Type([FromRoute]int id)
        {
            TransactionList model = new TransactionList(context);
            model.Transactions = await context.Transaction.OrderBy(s => s.Title.ToUpper()).Where(t => t.TransactionTypeId == id).ToListAsync();
            return View(model);
        }

        //Method: Purpose is to render the TransactionTypes view, which displays all Transaction categories
        public async Task<IActionResult> Types()
        {
            TransactionTypesViewModel model = new TransactionTypesViewModel(context);
            model.TransactionTypes = await context.TransactionType.OrderBy(s => s.Label).ToListAsync();
            model.TransactionSubTypes = await context.TransactionSubType.OrderBy(s => s.SubType).ToListAsync();
            //list of subcategories
            var subTypes = context.TransactionSubType.ToList();
            //cycle through each subcategory and define its Quantity as 
            //subCats.ForEach(sc => sc.Quantity = context.Transaction.Count(p => p.TransactionSubTypeId == sc.TransactionSubTypeId));
            // model.TransactionTypes = TransactionTypes;
            return View(model);
        }

        //Method: Purpose is to return the Error view
        public IActionResult Error()
        {
            return View();
        }

        //Method: Purpose is to create a new line item in the database when a customer clicks the "Add to Cart" button on a Transaction. Accepts an argument of the TransactionId, which is passed in through the post request attached to event listener on "Add to Cart" button
        
        }
    }

