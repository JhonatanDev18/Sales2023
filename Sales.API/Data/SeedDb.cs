using Microsoft.EntityFrameworkCore;
using Sales.API.Helpers;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Enums;
using Sales.Shared.Responses;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Jhonatan", "Soto", "soto@yopmail.com", "312 643 1715", "Calle Luna Calle Sol",
                UserType.Admin);
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<CountryResponse> countries = (List<CountryResponse>)responseCountries.Result!;
                    foreach (CountryResponse countryResponse in countries)
                    {
                        Country country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
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
                _context.Categories.Add(new Category { Name = "Consumo personal" });
                _context.Categories.Add(new Category { Name = "Drogas" });
                _context.Categories.Add(new Category { Name = "Dildos" });
                _context.Categories.Add(new Category { Name = "Juguetes de LEGO" });
                _context.Categories.Add(new Category { Name = "Juguetes de Cocina" });
                _context.Categories.Add(new Category { Name = "Juguetes de Armonia" });
                _context.Categories.Add(new Category { Name = "Juguetes sexuales" });
                _context.Categories.Add(new Category { Name = "Jugueste sexuales gays" });
                _context.Categories.Add(new Category { Name = "Televisores" });
                _context.Categories.Add(new Category { Name = "Computadores" });
                _context.Categories.Add(new Category { Name = "Celulares" });
                _context.Categories.Add(new Category { Name = "Videojuegos" });
                _context.Categories.Add(new Category { Name = "Electrodomésticos" });
                _context.Categories.Add(new Category { Name = "Audio" });
                _context.Categories.Add(new Category { Name = "Cámaras" });
                _context.Categories.Add(new Category { Name = "Casa Inteligente" });
                _context.Categories.Add(new Category { Name = "Accesorios" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Netflix y Pines" });
                _context.Categories.Add(new Category { Name = "Llantas" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Ofertas" });
                _context.Categories.Add(new Category { Name = "Otros" });
                _context.Categories.Add(new Category { Name = "Accesorios para celulares" });
                _context.Categories.Add(new Category { Name = "Smartwatch" });
                _context.Categories.Add(new Category { Name = "Tabletas" });
                _context.Categories.Add(new Category { Name = "Zona Empresarial" });
                _context.Categories.Add(new Category { Name = "Accesorios Computadores" });
                _context.Categories.Add(new Category { Name = "Impresoras y multifuncionales" });
                _context.Categories.Add(new Category { Name = "Monitores" });
                _context.Categories.Add(new Category { Name = "Proyectores" });
                _context.Categories.Add(new Category { Name = "Tintas y Papel" });
                _context.Categories.Add(new Category { Name = "Bose" });
                _context.Categories.Add(new Category { Name = "Instrumentos Musicales" });
                _context.Categories.Add(new Category { Name = "Salud y Bienestar" });
                _context.Categories.Add(new Category { Name = "Pc Gaming" });
                _context.Categories.Add(new Category { Name = "XBOX" });
                _context.Categories.Add(new Category { Name = "PlayStation" });
                _context.Categories.Add(new Category { Name = "Nintendo" });
                _context.Categories.Add(new Category { Name = "Membresías y Códigos digitales" });
                _context.Categories.Add(new Category { Name = "Colchones" });
                _context.Categories.Add(new Category { Name = "Muebles Para Armar" });
                _context.Categories.Add(new Category { Name = "Mesas" });
                _context.Categories.Add(new Category { Name = "Sofás y Sillones" });
                _context.Categories.Add(new Category { Name = "Sillas" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email,
            string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }
    }
}