using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    public partial class addView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               CREATE VIEW VmProductsImages
               AS 
SELECT dbo.TbProducts.ProductId, dbo.TbProducts.ProductName, dbo.TbProducts.SalesPrice, dbo.TbProducts.PurchasePrice, dbo.TbProducts.StockQuentity, dbo.TbSupplier.SupplierName, dbo.TbPharmcists.PharmcistName, 
                  TbProdcutsClassification_1.ProdcutsClassificationName, dbo.TbImages.ProductId as TbImages
FROM     dbo.TbProducts INNER JOIN
                  dbo.TbSupplier ON dbo.TbProducts.SupplierId = dbo.TbSupplier.SupplierId INNER JOIN
                  dbo.TbPharmcists ON dbo.TbProducts.PharmcistId = dbo.TbPharmcists.PharmcistId INNER JOIN
                  dbo.TbProdcutsClassification AS TbProdcutsClassification_1 ON dbo.TbProducts.ProdcutsClassificationId = TbProdcutsClassification_1.ProdcutsClassificationId INNER JOIN
                  dbo.TbImages ON dbo.TbProducts.ProductId = dbo.TbImages.ProductId
           ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW VmProductsImages");
        }
    }
}
