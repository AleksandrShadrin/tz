SELECT cat.Name As Categorie, prod.Name As Product FROM dbo.Categorie As cat
JOIN dbo.[Categorie-To-Product] link ON cat.Id = link.CategorieId
JOIN dbo.Product prod ON link.ProductId = prod.Id
