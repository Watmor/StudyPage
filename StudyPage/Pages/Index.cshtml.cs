using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyPage.Pages.Music;
using System;
using Microsoft.Data.SqlClient;

namespace StudyPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<MusicTrack> MusicTrack { get; set; } = new List<MusicTrack>();

        public void OnGet()
        {
            string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=music;Integrated Security=True;Pooling=False;TrustServerCertificate=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT MusicID, Name, MusicFile FROM music";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MusicTrack.Add(new MusicTrack
                        {
                            MusicID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MusicFile = reader.GetString(2)
                        });
                    }
                }
            }
        }
    }

}
