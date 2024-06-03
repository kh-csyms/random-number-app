#!/bin/sh
dotnet api/api.dll &
npm --prefix ui start &
wait
