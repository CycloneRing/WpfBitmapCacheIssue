#include <windows.h>
#include <iostream>
#include <dbghelp.h>

#define log(fmt,...)		printf(fmt "\n",__VA_ARGS__);
#define address_base		0x1000000
#define dll_name			"wpfgfx_v0400.dll"
#define pdb_path			"..\\Tools\\wpfgfx_v0400.pdb"
#define symbol_name			"g_fDirtyRegion_Enabled"

int main() 
{
	log("Initializing...");

	// Initialize the Symbol Handler
	if (!SymInitialize(GetCurrentProcess(), NULL, TRUE)) return EXIT_FAILURE;

	// Load the Symbols from the PDB file
	if (SymLoadModule64(GetCurrentProcess(), NULL, pdb_path, NULL, address_base, 0))
	{
		// Look up the Symbol
		SYMBOL_INFO* symbol = (SYMBOL_INFO*)calloc(sizeof(SYMBOL_INFO) + 1024, 1);
		if (symbol)
		{
			symbol->MaxNameLen = 1024;
			symbol->SizeOfStruct = sizeof(SYMBOL_INFO);
			SymFromName(GetCurrentProcess(), symbol_name, symbol);

			// The address of the variable is now in symbol->Address
			ULONG64 symOffset = symbol->Address - address_base;
			log("Symbol '%s' Offset Address as Hex : %p (%s + 0x%llX)", symbol_name, (void*)symOffset, dll_name, symOffset);
			log("Symbol '%s' Offset Address as Dec : %s + %lld", symbol_name, dll_name, symOffset);

			// Clean up
			free(symbol);
			SymCleanup(GetCurrentProcess());

			log("Executed with no error.");
			system("pause");
			return EXIT_SUCCESS;
		}
	}

	return EXIT_FAILURE;
}