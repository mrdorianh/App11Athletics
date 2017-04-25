##########################################################
# PLEASE READ:
#
# This script will be loaded by Visual Studio only when
# the solution is loaded therefore any changes you make
# to it will not be effective until after you exit Visual
# Studio and reload the solution.
##########################################################
function global:DeleteBinObj()
{
    Get-ChildItem .\ -include bin,obj -recu -Force | remove-item -force -recurse
}
