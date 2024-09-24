#pragma once

#include <Windows.h>

#define EXPORT __declspec(dllexport)

EXPORT INT CraftExecutable(LPCSTR, LPCSTR, LPCSTR, LPCSTR);