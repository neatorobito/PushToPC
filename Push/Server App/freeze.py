from cx_Freeze import setup, Executable

includes = ["tkinter", "tkinter.ttk", "threading", "ip"]

eggsacutibull = Executable(
    script = "Push Server.py",
    initScript = None,
    base = 'Win32GUI',
    targetName = "Push Server.exe",
    compress = True,
    copyDependentFiles = True,
    appendScriptToExe = False,
    appendScriptToLibrary = False,
    icon = "push.ico"
    )

setup(
        name = "Push Server",
        version = "0.1",
        author = 'Peak Software',
        description = "Server for the Push app for WP7.",
        options = {"build_exe": {"includes":includes}},
        executables = [eggsacutibull]
        )
