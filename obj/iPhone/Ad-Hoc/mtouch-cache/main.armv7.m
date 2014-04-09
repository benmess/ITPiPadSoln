#include "monotouch/main.h"

extern void *mono_aot_module_ITPiPadSoln_info;
extern void *mono_aot_module_monotouch_info;
extern void *mono_aot_module_mscorlib_info;
extern void *mono_aot_module_System_info;
extern void *mono_aot_module_System_Core_info;
extern void *mono_aot_module_System_Xml_info;
extern void *mono_aot_module_clsiOS_info;
extern void *mono_aot_module_clsTabletCommon_info;
extern void *mono_aot_module_Mono_Data_Sqlite_info;
extern void *mono_aot_module_System_Data_info;
extern void *mono_aot_module_System_Transactions_info;
extern void *mono_aot_module_System_Web_Services_info;

void monotouch_register_modules ()
{
	mono_aot_register_module (mono_aot_module_ITPiPadSoln_info);
	mono_aot_register_module (mono_aot_module_monotouch_info);
	mono_aot_register_module (mono_aot_module_mscorlib_info);
	mono_aot_register_module (mono_aot_module_System_info);
	mono_aot_register_module (mono_aot_module_System_Core_info);
	mono_aot_register_module (mono_aot_module_System_Xml_info);
	mono_aot_register_module (mono_aot_module_clsiOS_info);
	mono_aot_register_module (mono_aot_module_clsTabletCommon_info);
	mono_aot_register_module (mono_aot_module_Mono_Data_Sqlite_info);
	mono_aot_register_module (mono_aot_module_System_Data_info);
	mono_aot_register_module (mono_aot_module_System_Transactions_info);
	mono_aot_register_module (mono_aot_module_System_Web_Services_info);

}

void monotouch_register_assemblies ()
{
	monotouch_open_and_register ("monotouch.dll");
	monotouch_open_and_register ("clsiOS.dll");

}

void monotouch_setup ()
{
	use_old_dynamic_registrar = TRUE;
	monotouch_create_classes ();
	monotouch_assembly_name = "ITPiPadSoln.exe";
	mono_use_llvm = FALSE;
	monotouch_log_level = 0;
	monotouch_new_refcount = FALSE;
	monotouch_sgen = FALSE;
}

