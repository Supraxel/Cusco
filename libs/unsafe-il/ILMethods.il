#include "coreassembly.h"

.assembly Cusco.UnsafeIL
{
  .hash algorithm 0x00008004 // SHA1
  .ver 4:0:5:0
}

.module Cusco.UnsafeIL.dll
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.corflags 0x00000001 // = IL Only

.class public abstract sealed auto ansi beforefieldinit Cusco.UnsafeIL
{
  .method public hidebysig static void* AsPointerReadonly<valuetype T>([in] !!T& 'value') cil managed aggressiveinlining
  {
    .param [1]
    .custom instance void [System.Runtime]System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    ldarg.0
    conv.u
    ret
  }

  .method public hidebysig static !!T Add<valuetype T>([in] !!T 'lhs', [in] !!T 'rhs') cil managed aggressiveinlining
  {
    .param [2]
    .maxstack 2
    ldarg.0
    ldarg.1
    add
    ret
  }
}
