using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        public readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any()) 
            {
                _context.Countries.Add(new Country 
                { 
                    Name = "Colombia",
                    States = new List<State>
                    { 
                        new State 
                        { 
                            Name = "Antioquia",
                            Cities = new List<City> 
                            {
                                 new City { Name = "Medellín" },
                                 new City { Name = "Itagüí" },
                                 new City { Name = "Envigado" },
                                 new City { Name = "Bello" },
                                 new City { Name = "Rionegro" },
                                 new City { Name = "Copacabana" }
                            }
                        },
                        new State { Name = "Santander" },
                        new State
                        {
                            Name = "Bogotá",
                            Cities = new List<City>
                            {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" }
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country { Name = "Perú" }); 
                _context.Countries.Add(new Country { Name = "México" });
                _context.Countries.Add(new Country { Name = "Argentina" });
                _context.Countries.Add(new Country { Name = "Canadá" });
                _context.Countries.Add(new Country { Name = "Ecuador" });

                _context.Countries.Add(new Country 
                { 
                    Name = "Estados Unidos",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Florida",
                            Cities = new List<City>
                            {
                                 new City { Name = "Orlando" },
                                 new City { Name = "Miami" },
                                 new City { Name = "Tampa" },
                                 new City { Name = "Fort Lauderdale" },
                                 new City { Name = "Key West" }
                            },
                        },
                        new State
                        {
                            Name = "Texas",
                            Cities = new List<City>
                            {
                                 new City { Name = "Houston" },
                                 new City { Name = "San Antonio" },
                                 new City { Name = "Dallas" },
                                 new City { Name = "Austin" },
                                 new City { Name = "El Paso" }
                            },
                        },
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Abarrotes" });
                _context.Categories.Add(new Category { Name = "Productos enlatados" });
                _context.Categories.Add(new Category { Name = "Lácteos" });
                _context.Categories.Add(new Category { Name = "Botanas" });
                _context.Categories.Add(new Category { Name = "Confitería/Duceria" });
                _context.Categories.Add(new Category { Name = "Harinas" });
                _context.Categories.Add(new Category { Name = "Panaderia" });
                _context.Categories.Add(new Category { Name = "Frutas" });
                _context.Categories.Add(new Category { Name = "Verduras" });
                _context.Categories.Add(new Category { Name = "Bebidas" });
                _context.Categories.Add(new Category { Name = "Bebidas alcohólicas" });
                _context.Categories.Add(new Category { Name = "Alimentos procesados" });
                _context.Categories.Add(new Category { Name = "Automedicación" });
                _context.Categories.Add(new Category { Name = "Higiene personal" });
                _context.Categories.Add(new Category { Name = "Uso doméstico" });
                _context.Categories.Add(new Category { Name = "Helados" });
                _context.Categories.Add(new Category { Name = "Aseo" });

                await _context.SaveChangesAsync();
            }
        }
    }
}