using CoWorking.App.Models;
using CoWorkingApp.App.Enumerations;
using CoWorkingApp.Data;
using CoWorkingApp.Data.Tools;
using System;
using System.Globalization;
using System.Linq;

namespace CoWorkingApp.App.Logic
{
    public class UserService
    {
        private UserData userData {get; set;}
        private DeskData deskData {get; set;}
        private ReservationData reservationData {get; set;}

        public UserService(UserData userData, DeskData deskData)
        {
            this.userData = userData;
            this.deskData = deskData;
            // this.reservationData = new ReservationData();
            this.reservationData = reservationData;
        }

        public void ExcecuteAction(AdminUsers menuAdminUserSelected)
        {
            switch(menuAdminUserSelected)
                    {
                        case AdminUsers.Agregar:
                            User newUser = new User();
                            Console.WriteLine("Escriba el nombre");
                            newUser.Name = Console.ReadLine();
                            Console.WriteLine("Escriba el apellido");
                            newUser.LastName = Console.ReadLine();
                            Console.WriteLine("Escriba el email");
                            newUser.Email = Console.ReadLine();
                            Console.WriteLine("Escriba el password");
                            newUser.Password = EncrypData.GetPassword();
                            userData.CreateUser(newUser);
                            Console.WriteLine("USUARIO CREADO!");
                        break;
                        case AdminUsers.Editar:
                            Console.WriteLine("Escriba el correo del usuario");
                            var userFound = userData.FindUser(Console.ReadLine());
                            while(userFound == null)
                            {
                                Console.WriteLine("Escriba el correo del usuario");
                                userFound = userData.FindUser(Console.ReadLine());
                            }
                            Console.WriteLine("Escriba el nombre");
                            userFound.Name = Console.ReadLine();
                            Console.WriteLine("Escriba el apellido");
                            userFound.LastName = Console.ReadLine();
                            Console.WriteLine("Escriba el email");
                            userFound.Email = Console.ReadLine();
                            Console.WriteLine("Escriba el password");
                            userFound.Password = EncrypData.GetPassword();
                            userData.EditUser(userFound);
                            Console.WriteLine("Usuario Editado correctamente");
                        break;
                        case AdminUsers.Eliminar:
                              Console.WriteLine("Escriba el correo del usuario");
                            var userFoundDelete = userData.FindUser(Console.ReadLine());
                            while(userFoundDelete == null)
                            {
                                Console.WriteLine("Escriba el correo del usuario");
                                userFoundDelete = userData.FindUser(Console.ReadLine());
                            }
                            Console.WriteLine($"¿Está seguro que desea eliminar a {userFoundDelete.Name} {userFoundDelete.LastName}, SI, NO?");
                           if(Console.ReadLine() == "SI")
                           {
                               userData.DeleteUser(userFoundDelete.UserId);

                           }
                           Console.WriteLine("Usuario Eliminado correctamente");
                        break;
                        case AdminUsers.CambiarPassword:
                              Console.WriteLine("Escriba el correo del usuario");
                            var userFoundPassword = userData.FindUser(Console.ReadLine());
                            while(userFoundPassword == null)
                            {
                                Console.WriteLine("Escriba el correo del usuario");
                                userFound = userData.FindUser(Console.ReadLine());
                            }
                            Console.WriteLine("Escriba el password");
                            userFoundPassword.Password = EncrypData.GetPassword();
                            userData.EditUser(userFoundPassword);
                            Console.WriteLine("Contraseña cambiada correctamente");
                        break;
                    }
        }

        public User LoginUser(bool isAdmin)
        {
            bool loginResult = false;
                while(!loginResult)
                {
                    Console.WriteLine("Ingrese usuario");
                    var userLogin =  Console.ReadLine();
                    Console.WriteLine("Ingrese contraseña");
                    var passwordLogin = EncrypData.GetPassword();

                    var userLogged = userData.Login(userLogin, passwordLogin, isAdmin);
                    loginResult = userLogged != null;

                    if(!loginResult) Console.WriteLine("Usuario o contraseña incorrecta");

                    else return userLogged;

                }
                return null;

        }

        public void ExecutActionByUser(User user, MenuUser menuUserSelected)
        {
            switch(menuUserSelected)
                {
                    case MenuUser.ReservarPuesto:
                         var desklist = deskData.GetAvailableDesks();
                         Console.WriteLine("Puestos disponibles");
                         foreach(var item in desklist)
                         {
                             Console.WriteLine($"{item.Number} -{item.Descriptions}");
                         }
                         var newReservation = new Reservation();
                         Console.WriteLine("Ingrese el número de puesto");
                            var deskFound = deskData.FinDesk(Console.ReadLine());

                            while(deskFound == null)
                            {
                                Console.WriteLine("Escriba el número del puesto");
                                deskFound = deskData.FinDesk(Console.ReadLine());
                            } 
                            newReservation.DeskId = deskFound.DeskId;
                            var dateSelected = new DateTime();
                             while(dateSelected.Year == 0001)
                            {
                                Console.WriteLine("Ingrese la fecha de reserva (dd-mm-yyyy)");
                                DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out dateSelected );
                            } 
                            newReservation.ReservationDate = dateSelected;
                            newReservation.UserId = user.UserId;
                            reservationData.CreateReservation(newReservation);

                    Console.WriteLine("El puesto ha sido reservado");
                    break;
                    
                    case MenuUser.CancelarReserva:
                            Console.WriteLine("Estas son las reservaciones disponibles");
                            var userReservations = reservationData.GetReservationsByUser(user.UserId).ToList();
                            var deskUserList = deskData.GetAvailableDesks().ToList();
                            int indexReservation = 1;
                            foreach(var item in userReservations)
                            {
                                Console.WriteLine($"{indexReservation} - {deskUserList.FirstOrDefault(p => p.DeskId == item.DeskId).Number} - {item.ReservationDate.ToString("dd-MM-yyyy")}");
                                indexReservation++;
                            }
                            var indexReservationSelected =0;
                            while(indexReservationSelected < 1 || indexReservationSelected > indexReservation)
                            {
                                Console.WriteLine("Ingrese el número de la reservacion que desea eliminar");
                                indexReservationSelected = int.Parse(Console.ReadLine());

                            }
                            var reservationTodelete = userReservations[indexReservationSelected];
                            reservationData.CancelReservation(reservationTodelete.ReservationId);
                            Console.WriteLine("La reservación ha sido cancelada");
                    break;
                    case MenuUser.HistorialReservas:
                    Console.WriteLine("Opción ver el historial de reserva");


                    break;
                    case MenuUser.CambiarPassword:
                    Console.WriteLine("Opción cambiar contraseña");

                    
                    break;

                }
        }
    }
}
