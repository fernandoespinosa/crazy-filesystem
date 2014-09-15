using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harness
{
    class FileSystemFixture
    {
        private Random _random = new Random((int) DateTime.Now.Ticks);

        [Test]
        public void X()
        {
            var rootDir = @"C:\TEMP\ROOT";
            foreach (var dirPath in CreateCrazyFileSystemn_2(rootDir, 10))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        private IEnumerable<string> CreateCrazyFileSystemn_2(string rootDir, int maxDepth)
        {
            if (maxDepth == 0)
                return new[] { rootDir };
            var dirCount = _random.Next(1, 20);
            return Enumerable.Range(0, dirCount)
                .SelectMany(i => CreateCrazyFileSystemn_2(Path.Combine(rootDir, GetRandomString(2, 32)), _random.Next(maxDepth - 1)));
        }

        public string GetRandomString(int minLength, int maxLength)
        {
            var buffer = new byte[_random.Next(minLength, maxLength)];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte) _random.Next(65, 90);
            }
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
