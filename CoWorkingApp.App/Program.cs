using System;
using CoWorkingApp.Data;
using CoWorkingApp.App.Enumerations;
using CoWorkingApp.App.Logic;
using CoWorkingApp.Data.Tools;
using CoWorking.App.Models;

namespace CoWorkingApp.App
{
    class Program
    {
        static User ActiveUser {get; set;}
        static UserData UserDataServices { get; set;} = new UserData();
        static DeskData DeskDataSevice {get; set;} = new DeskData();
        static UserService UserLogicService {get; set;} = new UserService(UserDataServices, DeskDataSevice);
        static DeskService DeskLogicService {get; set;} = new DeskService(DeskDataSevice);
        static void Main(string[] args)
        {
            string rolSelected = "";
            Console.WriteLine("Bienvenido al Coworking");
            Console.WriteLine();
            while(rolSelected != "1" && rolSelected != "2")
            {
                Console.WriteLine("1=Admin, 2=Usuario");
                rolSelected = Console.ReadLine();
            }
            if(Enum.Parse<UserRole>(rolSelected) == UserRole.Admin)
            {
                UserLogicService.LoginUser(true);

                string menuAdminSelected = "";
                while(true)
                {
                while(menuAdminSelected != "1" && menuAdminSelected != "2")
                {
                    Console.WriteLine("1=Administracion de puestos, 2=Administracion de usuarios");
                    menuAdminSelected = Console.ReadLine();
                }
                if(Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdministracionPuestos)
                {
                    string menuPuestosSelected = "";
                    while(menuPuestosSelected != "1" && 
                          menuPuestosSelected != "2" && 
                          menuPuestosSelected != "3" && 
                          menuPuestosSelected != "4")
                          {
                              
                              Console.WriteLine("Aministracion de puestos");
                              Console.WriteLine("1=Crear,  2=Editar, 3=Eliminar, 4=Bloquear");                    
                              menuPuestosSelected = Console.ReadLine();
                          }
                          AdminPuestos menuAdminPuestosSelected = Enum.Parse<AdminPuestos>(menuPuestosSelected);
                          DeskLogicService.ExecuteAction(menuAdminPuestosSelected);
                    
                }
                else if(Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdministracionUsuarios)
                {
                    string menuUsuariosSelected = "";
                    while(menuUsuariosSelected != "1" && 
                          menuUsuariosSelected != "2" && 
                          menuUsuariosSelected != "3" && 
                          menuUsuariosSelected != "4")
                          {
                              Console.WriteLine("Aministracion de Usuarios");
                              Console.WriteLine("1=Crear,  2=Editar, 3=Eliminar, 4=Cambiar la contraseña");                    
                              menuUsuariosSelected = Console.ReadLine();
                          }

                    AdminUsers menuAdminUserSelected = Enum.Parse<AdminUsers>(menuUsuariosSelected);
                    UserLogicService.ExcecuteAction(menuAdminUserSelected);
                    // UserLogicService();

                }
                
            menuAdminSelected = "";
            }
            }
            else if(Enum.Parse<UserRole>(rolSelected) == UserRole.User)
            {
                //Login before actions
                ActiveUser = UserLogicService.LoginUser(false);

                while(true)
                {
                string menuUsuarioSelected = "";  

                while(menuUsuarioSelected !="1" &&
                      menuUsuarioSelected !="2" &&
                      menuUsuarioSelected !="3" &&
                      menuUsuarioSelected !="4" )
                {
                    Console.WriteLine("1=Reservar puesto, 2=Cancelar reserva, 3=Ver el historial de reserva, 4=Cambiar contraseña");
                    menuUsuarioSelected = Console.ReadLine();
                }

                MenuUser menuUserSelected = Enum.Parse<MenuUser>(menuUsuarioSelected);
                
                menuUsuarioSelected = " ";
                UserLogicService.ExecutActionByUser(ActiveUser, menuUserSelected);
            }

            }
            
        }
    
    }
}

