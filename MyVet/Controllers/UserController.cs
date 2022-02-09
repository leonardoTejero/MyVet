using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using MyVet.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVet.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRolServices _rolServices;

        //Inyeccion para poder usar los metodos de las interfaces
        public UserController(IUserServices userServices, IRolServices rolServices)
        {
            _userServices = userServices;
            _rolServices = rolServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            //FormAuthentication.
            List<UserEntity> users = _userServices.GetAll();
            return View(users);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //validacion del id que no sea nulo o que diga que no encontro el destino pero no se detenga la app
            IActionResult response;

            if (id == null)
                response = NotFound();

            UserEntity user = _userServices.GetUser((int)id);
            if (user == null)
            {
                response = NotFound();
            }
            else
            {
                response = View(user);
            }

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEntity user)
        {
            IActionResult response;

            bool result = await _userServices.UpdateUser(user);
            if (result)
            {
                response = RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo Actualizar el Usuario");
                response = RedirectToAction(nameof(Index));
            }
            return response;
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            IActionResult response;

            if (id == null)
                response = NotFound();

            UserEntity user = _userServices.GetUser((int)id);
            if (user == null)
            {
                response = NotFound();
            }
            else
            {
                response = View(user);
            }

            return response;
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int IdUser)
        {
            IActionResult response;
            if (IdUser == 0)
                response = NotFound();

            else
            {
                bool result = await _userServices.DeleteUser(IdUser);
                if (result)
                {
                    response = RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo Actualizar el Usuario");
                    response = RedirectToAction(nameof(Index));
                }
            }

            return response;
        }


        [HttpGet]
        public IActionResult Create()
        {
            //Llamar la lista de roles con los campos a mostrar y pasarlos a la vista de creacion
            ViewData["Roles"] = new SelectList(_rolServices.GetAll(), "IdRol", "Rol"); 
            //retorna la vista de creacion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserEntity user)
        {
            IActionResult response;

            ResponseDto result = await _userServices.CreateUser(user);
            if (result.IsSuccess)
            {
                response = RedirectToAction(nameof(Index));
            }
            else
            {
                //Recarga la opcion de Roles
                ViewData["Roles"] = new SelectList(_rolServices.GetAll(), "IdRol", "Rol");
                //Trae los mensajes de error
                ModelState.AddModelError(string.Empty, result.Message);
                response = View(user);
            }
            return response;
        }
    }
} 
