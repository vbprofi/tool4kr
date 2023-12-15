@echo off
set gitdir="C:\Users\pc\Documents\portable\git"
set path=%gitdir%\cmd;%path%
git config core.editor notepad
echo zuletzt modifizierte Dateien
git show --name-only
echo.
echo Anzahl der Commits
git rev-list --all --count
cmd