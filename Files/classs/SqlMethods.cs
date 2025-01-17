using ComunicaationToServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ComunicaationToServer.Methods;
  
  public class MetodosDobancodeDados : DadosDeMinhaAPI
    {
        public MySqlConnection Save()
        {
            string connstring = "Server=localhost;Port=3306;User Id=#Seu usuario#;Password=#Sua senha#;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = File.ReadAllText(@"C:\Visual Studio Projects\_NetProjects\C#\ComunicaationToServer\querys(sql)\MAIN.SQL");

                cmd.ExecuteNonQuery();
            }

            conn.Close();

            return conn;
        }

        public void add(string Nomes, int idade)
        {
            MetodosDobancodeDados aPI = new MetodosDobancodeDados();

            using (MySqlConnection conn = Save())
            {
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {

                        cmd.CommandText = "INSERT INTO API_UsersDB.Users (nome, idade) VALUES ('" + Nomes + "', '" + idade + "');";

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("HTTP.Response: Usuario adicionado com sucesso");
                    } catch (Exception e) {
                        Console.WriteLine($"HTTP.Error: {e.Message}");
                        return;
                    }
                }

                conn.Close();
            }

        }

        public void delete(int id)
        {
            MetodosDobancodeDados aPI = new MetodosDobancodeDados();

            using (MySqlConnection conn = Save())
            {
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "DELETE FROM API_UsersDB.Users WHERE id='" + id +"';";

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("HTTP.Response: Usuario deletado com sucesso");
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"HTTP.Error : {e.Message}");
                        throw;
                    }
                }

                conn.Close();
            }

        }

        public void update(string Nome, int idade, int id)
        {

            using (MySqlConnection conn = Save())
            {
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "UPDATE Users SET nome = '" + Nome + "', idade = '" + idade + "' WHERE id = '" + id + "';";

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("HTTP.Response: Usuario atualizado com sucesso");

                    } catch (Exception e)
                    {
                        Console.WriteLine($"HTTP.Error: {e.Message}");
                        return;
                    }


                }

                conn.Close();
            }
        }

        public JsonResult GetAllData()
        {
            DadosDeMinhaAPI aPI = new DadosDeMinhaAPI();

            aPI.Nomes.Clear();
            aPI.idades.Clear();

            using (MySqlConnection conn = Save())
            {
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT nome, idade FROM API_UsersDB.Users";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aPI.Nomes.Add(reader.GetString("nome"));
                            aPI.idades.Add(reader.GetInt32("idade"));
                        }
                    }
                }

                conn.Close();
                JsonResult result = new JsonResult(aPI);
                
                return result;
            }
        }


    }
