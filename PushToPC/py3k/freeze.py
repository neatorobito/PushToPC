from cx_Freeze import setup, Executable

includes = ["tkinter", "tkinter.ttk", "threading", "ip"]

eggsacutibull = Executable(
    script = "PushToPC Server.py",
    initScript = None,
    base = 'Win32GUI',
    targetName = "PushToPC Server.exe",
    compress = True,
    copyDependentFiles = True,
    appendScriptToExe = False,
    appendScriptToLibrary = False,
    icon = "push.ico"
    )

setup(
        name = "PushToPC Server",
        version = "0.1",
        author = 'Peak Digital Group',
        description = "Server for the PushToPC app for WP7.",
        options = {"build_exe": {"includes":includes}},
        executables = [eggsacutibull]
        )
