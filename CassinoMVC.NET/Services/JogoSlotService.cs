using CassinoMVC.Models;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Serviço do caça-níqueis: executa giros usando as regras e padrões definidos em SlotMachine.
    /// </summary>
    public static class JogoSlotService
    {
        /// <summary>
        /// Executa um giro. Usa os padrões internos da SlotMachine (7 símbolos e 5 rolos).
        /// </summary>
        public static SlotMachine Girar()
        {
            var slot = new SlotMachine();
            slot.Girar(); // usa padrão de 5 rolos
            return slot;
        }
    }
}