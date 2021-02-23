using CoWorking.App.Models;
using CoWorking.App.Models.Enumerations;
using CoWorkingApp.App.Enumerations;
using CoWorkingApp.Data;
using CoWorkingApp.Data.Tools;
using System;
namespace CoWorkingApp.App.Logic
{
    public class DeskService
    {
        private DeskData deskData {get; set;}
        public DeskService(DeskData deskData)
        {
            this.deskData = deskData;
        }

       
        public void ExecuteAction(AdminPuestos adminDeskAction)
        {
             switch(adminDeskAction)
                    {
                        case AdminPuestos.Agregar:
                            Desk newDesk = new Desk();  
                            Console.WriteLine("Ingrese el numero, ejemplo: A-001");
                            newDesk.Number = Console.ReadLine();
                            Console.WriteLine("Ingrese una descripción");
                            newDesk.Descriptions = Console.ReadLine();
                            deskData.CreateDesk(newDesk);
                            Console.WriteLine("Puesto creado con èxito");
                        break;
                        case AdminPuestos.Editar:
                            Console.WriteLine("Ingrese el numero de puesto");
                            var deskFound = deskData.FinDesk(Console.ReadLine());
                             while(deskFound == null)
                            {
                                Console.WriteLine("Escriba el número del puesto");
                                deskFound = deskData.FinDesk(Console.ReadLine());
                            }
                            Console.WriteLine("Ingrese el numero, ejemplo: A-001");
                            deskFound.Number = Console.ReadLine();
                            Console.WriteLine("Ingrese una descripción");
                            deskFound.Descriptions = Console.ReadLine();
                            Console.WriteLine("Ingrese el estado del puesto, 1=Activo, 2=Inactivo, 3= Bloqueado");
                            deskFound.DeskStatus = Enum.Parse<DeskStatus>(Console.ReadLine());
                            deskData.EditDesk(deskFound);
                            Console.WriteLine("Puesto editado con éxito");
                        break;
                        case AdminPuestos.Eliminar:
                            Console.WriteLine("Ingrese el numero de puesto");
                            var deskFoundDelete = deskData.FinDesk(Console.ReadLine());
                             while(deskFoundDelete == null)
                            {
                                Console.WriteLine("Escriba el número del puesto");
                                deskFoundDelete = deskData.FinDesk(Console.ReadLine());
                            }
                            deskData.DeleteDesk(deskFoundDelete.DeskId);
                        Console.WriteLine("Puesto Eliminado correctamente");
                        break;
                        case AdminPuestos.Bloquear:
                        Console.WriteLine("Ingrese el numero de puesto");
                            var deskFoundBlock = deskData.FinDesk(Console.ReadLine());
                             while(deskFoundBlock == null)
                            {
                                Console.WriteLine("Escriba el número del puesto");
                                deskFoundBlock = deskData.FinDesk(Console.ReadLine());
                            }           
                            deskFoundBlock.DeskStatus = DeskStatus.Blocked;       
                            deskData.EditDesk(deskFoundBlock);                     
                            Console.WriteLine("Puesto bloqueado con èxito");
                        break;
                    }

        }

    }
}