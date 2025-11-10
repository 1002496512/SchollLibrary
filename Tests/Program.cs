using LibraryModels;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Hashing3();
        }

        static void ModelValidation()
        {
            Author author = new Author();   
            author.AuthorFirstName = "john";
            author.AuthorLastName = "dw";
            author.AuthorYear = 12;
            author.CountryId = 1;
            author.AuthorPicture = "picture.pdf";

            Dictionary<string, List<string>> errors = author.AllErrors();   
            if(author.IsValid == false)
            {
                foreach(var error in errors)
                {
                    foreach(var errorMsg in error.Value)
                    {
                        Console.WriteLine($"{errorMsg}");
                    }
                }
            }

        }

        static void Hashing()
        {
            string salt = "x8F2aB!";
            string password = "password123";
            string combined = salt + password;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combined);
                byte[] hash = sha256.ComputeHash(bytes);

                // המרה להקסה לקריאה נוחה
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                Console.WriteLine("SHA256 Hash: " + sb.ToString());
            }

        }

        static void Hashing3()
        {
            for(int i = 0; i < 5; i++)
            {

                string salt = "kfgk  kfjgk";//CreateSalt(16);
            string password = "password123";
            string combined = salt + password;

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(combined);
                    byte[] hash = sha256.ComputeHash(bytes);
                    //sha256.
                    // המרה להקסה לקריאה נוחה
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hash)
                        sb.Append(b.ToString("x2"));

                    Console.WriteLine("Hash: " + sb.ToString());
                    Console.WriteLine("Salt: " + salt);
                }
            }

        }
        public static string CreateSalt(int size = 16) // Default salt size in bytes
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] saltBytes = new byte[size];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }
        static void Hashing2()
        {
            string paaword = "alex1234";
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(paaword, out passwordHash, out passwordSalt);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbSalt = new StringBuilder();
            foreach (byte b in passwordHash)
                sb.Append(b.ToString("x2"));
            foreach (byte b in passwordSalt)
                sbSalt.Append(b.ToString("x2"));

            Console.WriteLine("Password Hash: " + sb.ToString());
            Console.WriteLine("Password Salt: " + sbSalt.ToString());
        }
    }


    // Provides methods for creating and verifying password hashes using HMACSHA512.
    public static class PasswordHasher
    {
        // Creates a hashed version of the provided password along with a unique salt.
        // password: The plaintext password to be hashed.
        // passwordHash: Outputs the resulting password hash as a byte array.
        // passwordSalt: Outputs the unique salt used in hashing as a byte array.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Instantiate HMACSHA512 to generate a cryptographic hash and a unique key (salt).
            using (var hmac = new HMACSHA256())
            {
                // The Key property of HMACSHA512 provides a randomly generated salt.
                passwordSalt = hmac.Key; // Assign the generated salt to the output parameter.

                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the password bytes using the HMACSHA512 instance.
                passwordHash = hmac.ComputeHash(passwordBytes); // Assign the computed hash to the output parameter.
            }
        }

        // Verifies whether the provided password matches the stored hash using the stored salt.
        // password: The plaintext password to verify.
        // storedHash: The stored password hash to compare against.
        // storedSalt: The stored salt used during the original hashing process.
        // Return: True if the password is valid and matches the stored hash; otherwise, false.
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Instantiate HMACSHA512 with the stored salt as the key to ensure the same hashing parameters.
            using (var hmac = new HMACSHA256(storedSalt))
            {
                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the password bytes using the HMACSHA512 instance initialized with the stored salt.
                byte[] computedHash = hmac.ComputeHash(passwordBytes);

                // Compare the computed hash with the stored hash byte by byte.
                // SequenceEqual ensures that both byte arrays are identical in sequence and value.
                bool hashesMatch = computedHash.SequenceEqual(storedHash);

                // Return the result of the comparison.
                return hashesMatch;
            }
        }
    }

}
