using ExpenseManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        // Simulating a database with a static list of expenses
        private static List<Expense> Expenses = new List<Expense>
        {
            new Expense { Id = 1, Category = "Entertainment", Amount = 500m, Date = DateTime.Now.AddDays(-1), Description = "Movie" },
            new Expense { Id = 2, Category = "Transport", Amount = 20.50m, Date = DateTime.Now.AddDays(-2), Description = "Petrol" },
            new Expense { Id = 3, Category = "Food", Amount = 50.75m, Date = DateTime.Now.AddDays(-1), Description = "Lunch" },
            new Expense { Id = 4, Category = "Education", Amount = 20.50m, Date = DateTime.Now.AddDays(-2), Description = "Diploma Fees" },
            new Expense { Id = 5, Category = "Stationary", Amount = 20.50m, Date = DateTime.Now.AddDays(-2), Description = "Pen" }
        };

        // GET: api/expenses
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpenses()
        {
            return Ok(Expenses);
        }

        // GET: api/expenses/{id}
        [HttpGet("{id}")]
        public ActionResult<Expense> GetExpense(int id)
        {
            var expense = Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        // POST: api/expenses
        [HttpPost]
        public ActionResult<Expense> CreateExpense([FromBody] Expense expense)
        {
            expense.Id = Expenses.Max(e => e.Id) + 1; // Simulate auto-incrementing ID
            Expenses.Add(expense);
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateExpense(int id, [FromBody] Expense updatedExpense)
        {
            var expense = Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            expense.Category = updatedExpense.Category;
            expense.Amount = updatedExpense.Amount;
            expense.Date = updatedExpense.Date;
            expense.Description = updatedExpense.Description;

            return NoContent();
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteExpense(int id)
        {
            var expense = Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            Expenses.Remove(expense);
            return NoContent();
        }
    }
}
