// Importamos los modelos de vista necesarios y las clases de identidad de ASP.NET Core
using BlogFitnessApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogFitnessApp.Controllers
{
    public class AccountController : Controller
    {

        // Inyeccion de dependencia del UserManager, que permite gestionar los usuarios
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager
            )
        {
            // Inicializamos el UserManager para poder gestionar usuarios
            this.userManager = userManager;
            // y el SignInManager para manejar el inicio de sesion
            this.signInManager = signInManager;
        }

        // Retorna la vista para que el usuario pueda registrarse
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Recive los datos del formulario
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

         
            // Creamos un nuevo usuario con los datos del formulario (nombre de usuario y correo)
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };

            // Se Intenta crear el usuario en la base de datos usando el UserManager
            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                //Asignar el rol User por defecto a este nuevo usuario
               var roleIdentityResult =   await userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }

            return View("Login");
        }


        //Retorna la vista de Login 
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }


        //Recibe los datos del formulario de inicio de sesion
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            // Intenta iniciar sesion con el UserManager usando el nombre de usuario y contraseña
          var signInResult = await signInManager.PasswordSignInAsync(
              loginViewModel.UserName, 
              loginViewModel.Password, 
              false, false
              );

            if (signInResult != null && signInResult.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginViewModel.UserName);
                var roles = await userManager.GetRolesAsync(user);

                // Si el usuario es administrador, redirigir a AdminTags/Add
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Add", "AdminTags");
                }

                // Redirigir a ReturnUrl si existe
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        //Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
              // Cierra la sesian del usuario actual
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Acceso Denegado
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}




// IdentityUser     | Representa un usuario (se puede extender).                                  
// IdentityRole     | Define roles como "admin", "superadmin", etc.                               
// UserManager<T>   | Gestiona usuarios (crear, actualizar, borrar, etc.).                        
// SignInManager<T> | Maneja el inicio y cierre de sesión.                                        
// RoleManager<T>   | Administra roles.                                                           
// DbContext        | IdentityDbContext,  gestiona tablas como AspNetUsers, AspNetRoles, etc.

