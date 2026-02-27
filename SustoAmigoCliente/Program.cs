using System.Net.Sockets;
using System.Text;

namespace SustoAmigoCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            string? ipServidor = "";
            int porta = 0;
          //  ConsoleKey teclaAtivacao = ConsoleKey.Spacebar; // tecla que ativa o susto

            while (true)
            {
                Console.WriteLine("=== SustoAmigoCliente ===");
                Console.WriteLine("Digite o IP do servidor (ou 'reset' para limpar):");
                ipServidor = Console.ReadLine();

                if (ipServidor?.ToLower() == "reset")
                {
                    Console.Clear();
                    continue; // volta ao início do loop
                }

                Console.WriteLine("Digite a porta (ou 'reset' para limpar):");
                string? portaStr = Console.ReadLine();

                if (portaStr?.ToLower() == "reset")
                {
                    Console.Clear();
                    continue;
                }

                if (!int.TryParse(portaStr, out porta))
                {
                    Console.WriteLine("Porta inválida. Tente novamente.\n");
                    continue;
                }

                Console.WriteLine($"Configuração definida: IP={ipServidor}, Porta={porta}");
                Console.WriteLine($"Digite now para enviar o susto. Digite 'reset' para reconfigurar.\n");

                while (true)
                {
                    string? entrada = Console.ReadLine();

                    if (entrada?.ToLower() == "reset")
                    {
                        Console.Clear();
                        break; // sai do loop interno e volta para pedir IP/porta
                    }

                    if (entrada == "now")
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ipServidor))
                            {
                                Console.WriteLine("IP do servidor não informado.");
                                return;
                            }

                            using (TcpClient cliente = new TcpClient(ipServidor, porta))
                            using (NetworkStream stream = cliente.GetStream())
                            {
                                byte[] dados = Encoding.UTF8.GetBytes("SUSTO");
                                stream.Write(dados, 0, dados.Length);
                            }

                            Console.WriteLine("Comando 'SUSTO' enviado!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao enviar comando: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}