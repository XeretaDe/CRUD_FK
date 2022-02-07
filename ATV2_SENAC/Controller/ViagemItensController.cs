using ATV2_SENAC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;
using ATV2_SENAC.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace atv2.Controllers
{
    public class ViagemItensController : Controller
    {
        private readonly Data _contexto;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public ViagemItensController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                Data contexto)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _contexto = contexto;

        }

        // GET: HomeController
        public IActionResult Index()
        {
            using (DbCommand cmd = _contexto.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT aspnetusers.UserName as NomeLogin, aspnetusers.Email as NomeUsuario, pacotesturisticos.Usuario, " +
                                    "pacotesturisticos.Id, pacotesturisticos.Nome, pacotesturisticos.Origem, " +
                                     "pacotesturisticos.Destino, pacotesturisticos.Atrativos, pacotesturisticos.Saida, " +
                                     "pacotesturisticos.Retorno " +
                                      "FROM pacotesturisticos " +
                                      "LEFT JOIN aspnetusers " +
                                      "ON aspnetusers.Id = pacotesturisticos.Usuario;";
                _contexto.Database.OpenConnection();
                List<ViagemItens> Itens = new List<ViagemItens>();
                using (DbDataReader ddr = cmd.ExecuteReader())
                { 

                    while (ddr.Read())
                    {
                        
                        Itens.Add(new ViagemItens
                        {
                            Id = ddr.GetInt32("Id"),
                            nome = ddr.GetString("Nome"),
                            Origem = ddr.GetString("Origem"),
                            Destino = ddr.GetString("Destino"),
                            Atrativos = ddr.GetString("Atrativos"),
                            Retorno = Convert.ToDateTime(ddr.GetDateTime("Retorno")),
                            Saida = Convert.ToDateTime(ddr.GetDateTime("Saida")),

                            Usuario = ddr.GetString("Usuario"),
                            nomeUsuario = ddr.GetString("NomeUsuario"),
                            LoginUser = ddr.GetString("NomeLogin")
                        });
                    }
                    return View(Itens);
                }


            }
        }


        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViagemItens Itens, Usuario user)
        {
            using (DbCommand cmd2 = _contexto.Database.GetDbConnection().CreateCommand())
            {
                cmd2.CommandText = "SELECT Id, UserName FROM aspnetusers WHERE UserName = '" + User.Identity.Name + "'";
                _contexto.Database.OpenConnection();
                using (DbDataReader ddr = cmd2.ExecuteReader())
                {
                    while (ddr.Read())
                    {
                        Itens.Usuario = ddr.GetString("Id");
                    }
                }
            }
            await _contexto.Viagens.AddAsync(Itens);
            await _contexto.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        // GET: HomeController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViagemItens Viagem = await _contexto.Viagens.FindAsync(id);
            Viagem.Usuario = Viagem.Usuario;
            return View(Viagem);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ViagemItens Itens)
        {
            _contexto.Viagens.Update(Itens);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ViagemItens viagem = await _contexto.Viagens.FindAsync(id);
            _contexto.Viagens.Remove(viagem);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
