using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MDpro.Models;

namespace MDpro.Controllers
{
    public class ProductController : Controller
    {
        string connectionstring = @"Data Source=DESKTOP-K3H6BAL\SQLEXPRESS;Initial Catalog=MvcCrudDB;Integrated Security=True";

       [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * From Product", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
                return View(dtblProduct);
        }

       
      [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productmodel)
        {
            using ( SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "insert into Product values(@ProductName,@Price,@Count)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productmodel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productmodel.Count);
                sqlCmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "Select * from Product where ProductID = @ProductID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            { 
                productModel.ProductId = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "Update Product Set ProductName = @ProductName , Price = @Price , Count = @Count where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@ProductID", productmodel.ProductId);
                sqlCmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productmodel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productmodel.Count);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "Delete from Product where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@ProductID",id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

         

    }
}
