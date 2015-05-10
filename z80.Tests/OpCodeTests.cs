﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace z80.Tests
{
    [TestFixture]
    public class OpCodeTests
    {
        private System en;
        private Z80Asm asm;
        private byte[] _ram;

        [Test]
        public void Test_Halt()
        {
            asm.Halt();

            en.Run();

            Assert.AreEqual(asm.WritePointer, en.PC);
        }

        [Test]
        public void Test_Noop()
        {
            asm.Noop();
            asm.Halt();

            en.Run();

            Assert.AreEqual(asm.WritePointer, en.PC);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(7)]
        public void Test_LD_R_N(int r)
        {
            asm.LdR(r,42);
            asm.Halt();

            en.Run();

            Assert.AreEqual(asm.WritePointer, en.PC);
            Assert.AreEqual(42, en.Reg8(r));
        }


        [Test]
        #region testcases
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(7, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(7, 2)]
        [TestCase(0, 3)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(7, 3)]
        [TestCase(0, 4)]
        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 4)]
        [TestCase(5, 4)]
        [TestCase(7, 4)]
        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(2, 5)]
        [TestCase(3, 5)]
        [TestCase(4, 5)]
        [TestCase(5, 5)]
        [TestCase(7, 5)]
        [TestCase(0, 7)]
        [TestCase(1, 7)]
        [TestCase(2, 7)]
        [TestCase(3, 7)]
        [TestCase(4, 7)]
        [TestCase(5, 7)]
        [TestCase(7, 7)]
        #endregion
        public void Test_LD_R1_R2(int r, int r2)
        {
            asm.LdR(r,33);
            asm.LdRR(r2, r);
            asm.Halt();

            en.Run();

            Assert.AreEqual(asm.WritePointer, en.PC);
            Assert.AreEqual(33, en.Reg8(r));
            Assert.AreEqual(33, en.Reg8(r2));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(7)]
        public void Test_LD_R_HL(int r)
        {
            asm.LdR16(2,5);
            asm.LdRHl(r);
            asm.Halt();
            asm.Data(123);

            en.Run();

            Assert.AreEqual(asm.WritePointer-1, en.PC);
            Assert.AreEqual(123, en.Reg8(r));
        }

        //////////////////////////
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _ram = new byte[0xFFFF];
            en = new System(_ram);
            asm = new Z80Asm(_ram);
        }
        [SetUp]
        public void TestSetup()
        {
            en.Reset();
            asm.Reset();
        }
    }
}