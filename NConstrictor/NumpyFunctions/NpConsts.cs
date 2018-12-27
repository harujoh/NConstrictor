namespace NConstrictor
{
    //from numpy/core/code_generators/numpy_api.py

    public class MultiarrayGlobalVars
    {
        const int NPY_NUMUSERTYPES = 7; // int
        const int NPY_DEFAULT_ASSIGN_CASTING = 292; // NPY_CASTING
    }

    public class MultiarrayScalarBoolValues
    {
        const int PY_ARRAY_SCALAR_BOOL_VALUES = 9;
    }

    public class NpConsts
    {
        public const int NPY_ARRAY_C_CONTIGUOUS = 0x0001;
        public const int NPY_ARRAY_F_CONTIGUOUS = 0x0002;
        public const int NPY_ARRAY_OWNDATA = 0x0004;
        public const int NPY_ARRAY_FORCECAST = 0x0010;
        public const int NPY_ARRAY_ENSURECOPY = 0x0020;
        public const int NPY_ARRAY_ENSUREARRAY = 0x0040;
        public const int NPY_ARRAY_ELEMENTSTRIDES = 0x0080;
        public const int NPY_ARRAY_ALIGNED = 0x0100;
        public const int NPY_ARRAY_NOTSWAPPED = 0x0200;
        public const int NPY_ARRAY_WRITEABLE = 0x0400;
        public const int NPY_ARRAY_UPDATEIFCOPY = 0x1000; /* Deprecated in 1.14 */
        public const int NPY_ARRAY_WRITEBACKIFCOPY = 0x2000;
        public const int NPY_ARRAY_BEHAVED = (NPY_ARRAY_ALIGNED | NPY_ARRAY_WRITEABLE);
        public const int NPY_ARRAY_BEHAVED_NS = (NPY_ARRAY_ALIGNED | NPY_ARRAY_WRITEABLE | NPY_ARRAY_NOTSWAPPED);
        public const int NPY_ARRAY_CARRAY = (NPY_ARRAY_C_CONTIGUOUS | NPY_ARRAY_BEHAVED);
        public const int NPY_ARRAY_CARRAY_RO = (NPY_ARRAY_C_CONTIGUOUS | NPY_ARRAY_ALIGNED);
        public const int NPY_ARRAY_FARRAY = (NPY_ARRAY_F_CONTIGUOUS | NPY_ARRAY_BEHAVED);
        public const int NPY_ARRAY_FARRAY_RO = (NPY_ARRAY_F_CONTIGUOUS | NPY_ARRAY_ALIGNED);
        public const int NPY_ARRAY_DEFAULT = (NPY_ARRAY_CARRAY);
        public const int NPY_ARRAY_IN_ARRAY = (NPY_ARRAY_CARRAY_RO);
        public const int NPY_ARRAY_OUT_ARRAY = (NPY_ARRAY_CARRAY);
        public const int NPY_ARRAY_INOUT_ARRAY = (NPY_ARRAY_CARRAY | NPY_ARRAY_UPDATEIFCOPY);
        public const int NPY_ARRAY_INOUT_ARRAY2 = (NPY_ARRAY_CARRAY | NPY_ARRAY_WRITEBACKIFCOPY);
        public const int NPY_ARRAY_IN_FARRAY = (NPY_ARRAY_FARRAY_RO);
        public const int NPY_ARRAY_OUT_FARRAY = (NPY_ARRAY_FARRAY);
        public const int NPY_ARRAY_INOUT_FARRAY = (NPY_ARRAY_FARRAY | NPY_ARRAY_UPDATEIFCOPY);
        public const int NPY_ARRAY_INOUT_FARRAY2 = (NPY_ARRAY_FARRAY | NPY_ARRAY_WRITEBACKIFCOPY);
        public const int NPY_ARRAY_UPDATE_ALL = (NPY_ARRAY_C_CONTIGUOUS | NPY_ARRAY_F_CONTIGUOUS | NPY_ARRAY_ALIGNED);
        public const int NPY_ARR_HAS_DESCR = 0x0800;
    }

    public class MultiarrayTypesAPI
    {
        public const int PyBigArray_Type = 1;
        public const int PyArray_Type = 2;
        public const int PyArrayDescr_Type = 3;
        public const int PyArrayFlags_Type = 4;
        public const int PyArrayIter_Type = 5;
        public const int PyArrayMultiIter_Type = 6;
        public const int PyBoolArrType_Type = 8;
        public const int PyGenericArrType_Type = 10;
        public const int PyNumberArrType_Type = 11;
        public const int PyIntegerArrType_Type = 12;
        public const int PySignedIntegerArrType_Type = 13;
        public const int PyUnsignedIntegerArrType_Type = 14;
        public const int PyInexactArrType_Type = 15;
        public const int PyFloatingArrType_Type = 16;
        public const int PyComplexFloatingArrType_Type = 17;
        public const int PyFlexibleArrType_Type = 18;
        public const int PyCharacterArrType_Type = 19;
        public const int PyByteArrType_Type = 20;
        public const int PyShortArrType_Type = 21;
        public const int PyIntArrType_Type = 22;
        public const int PyLongArrType_Type = 23;
        public const int PyLongLongArrType_Type = 24;
        public const int PyUByteArrType_Type = 25;
        public const int PyUShortArrType_Type = 26;
        public const int PyUIntArrType_Type = 27;
        public const int PyULongArrType_Type = 28;
        public const int PyULongLongArrType_Type = 29;
        public const int PyFloatArrType_Type = 30;
        public const int PyDoubleArrType_Type = 31;
        public const int PyLongDoubleArrType_Type = 32;
        public const int PyCFloatArrType_Type = 33;
        public const int PyCDoubleArrType_Type = 34;
        public const int PyCLongDoubleArrType_Type = 35;
        public const int PyObjectArrType_Type = 36;
        public const int PyStringArrType_Type = 37;
        public const int PyUnicodeArrType_Type = 38;
        public const int PyVoidArrType_Type = 39;
        // End 1.5 API
        public const int PyTimeIntegerArrType_Type = 214;
        public const int PyDatetimeArrType_Type = 215;
        public const int PyTimedeltaArrType_Type = 216;
        public const int PyHalfArrType_Type = 217;
        public const int NpyIter_Type = 218;
        // End 1.6 API
    }

    public class MultiarrayFuncsAPI
    {
        public const int PyArray_GetNDArrayCVersion = 0;
        public const int PyArray_SetNumericOps = 40;
        public const int PyArray_GetNumericOps = 41;
        public const int PyArray_INCREF = 42;
        public const int PyArray_XDECREF = 43;
        public const int PyArray_SetStringFunction = 44;
        public const int PyArray_DescrFromType = 45;
        public const int PyArray_TypeObjectFromType = 46;
        public const int PyArray_Zero = 47;
        public const int PyArray_One = 48;
        public const int PyArray_CastToType = 49;// StealRef(2), NonNull(2)
        public const int PyArray_CastTo = 50;
        public const int PyArray_CastAnyTo = 51;
        public const int PyArray_CanCastSafely = 52;
        public const int PyArray_CanCastTo = 53;
        public const int PyArray_ObjectType = 54;
        public const int PyArray_DescrFromObject = 55;
        public const int PyArray_ConvertToCommonType = 56;
        public const int PyArray_DescrFromScalar = 57;
        public const int PyArray_DescrFromTypeObject = 58;
        public const int PyArray_Size = 59;
        public const int PyArray_Scalar = 60;
        public const int PyArray_FromScalar = 61;// StealRef(2)
        public const int PyArray_ScalarAsCtype = 62;
        public const int PyArray_CastScalarToCtype = 63;
        public const int PyArray_CastScalarDirect = 64;
        public const int PyArray_ScalarFromObject = 65;
        public const int PyArray_GetCastFunc = 66;
        public const int PyArray_FromDims = 67;
        public const int PyArray_FromDimsAndDataAndDescr = 68;// StealRef(3)
        public const int PyArray_FromAny = 69;// StealRef(2)
        public const int PyArray_EnsureArray = 70;// StealRef(1)
        public const int PyArray_EnsureAnyArray = 71;// StealRef(1)
        public const int PyArray_FromFile = 72;
        public const int PyArray_FromString = 73;
        public const int PyArray_FromBuffer = 74;
        public const int PyArray_FromIter = 75;// StealRef(2)
        public const int PyArray_Return = 76;// StealRef(1)
        public const int PyArray_GetField = 77;// StealRef(2), NonNull(2)
        public const int PyArray_SetField = 78;// StealRef(2), NonNull(2)
        public const int PyArray_Byteswap = 79;
        public const int PyArray_Resize = 80;
        public const int PyArray_MoveInto = 81;
        public const int PyArray_CopyInto = 82;
        public const int PyArray_CopyAnyInto = 83;
        public const int PyArray_CopyObject = 84;
        public const int PyArray_NewCopy = 85;// NonNull(1));//
        public const int PyArray_ToList = 86;
        public const int PyArray_ToString = 87;
        public const int PyArray_ToFile = 88;
        public const int PyArray_Dump = 89;
        public const int PyArray_Dumps = 90;
        public const int PyArray_ValidType = 91;
        public const int PyArray_UpdateFlags = 92;
        public const int PyArray_New = 93;// NonNull(1)
        public const int PyArray_NewFromDescr = 94;// StealRef(2), NonNull([1, 2])
        public const int PyArray_DescrNew = 95;
        public const int PyArray_DescrNewFromType = 96;
        public const int PyArray_GetPriority = 97;
        public const int PyArray_IterNew = 98;
        public const int PyArray_MultiIterNew = 99;
        public const int PyArray_PyIntAsInt = 100;
        public const int PyArray_PyIntAsIntp = 101;
        public const int PyArray_Broadcast = 102;
        public const int PyArray_FillObjectArray = 103;
        public const int PyArray_FillWithScalar = 104;
        public const int PyArray_CheckStrides = 105;
        public const int PyArray_DescrNewByteorder = 106;
        public const int PyArray_IterAllButAxis = 107;
        public const int PyArray_CheckFromAny = 108;// StealRef(2)
        public const int PyArray_FromArray = 109;// StealRef(2)
        public const int PyArray_FromInterface = 110;
        public const int PyArray_FromStructInterface = 111;
        public const int PyArray_FromArrayAttr = 112;
        public const int PyArray_ScalarKind = 113;
        public const int PyArray_CanCoerceScalar = 114;
        public const int PyArray_NewFlagsObject = 115;
        public const int PyArray_CanCastScalar = 116;
        public const int PyArray_CompareUCS4 = 117;
        public const int PyArray_RemoveSmallest = 118;
        public const int PyArray_ElementStrides = 119;
        public const int PyArray_Item_INCREF = 120;
        public const int PyArray_Item_XDECREF = 121;
        public const int PyArray_FieldNames = 122;
        public const int PyArray_Transpose = 123;
        public const int PyArray_TakeFrom = 124;
        public const int PyArray_PutTo = 125;
        public const int PyArray_PutMask = 126;
        public const int PyArray_Repeat = 127;
        public const int PyArray_Choose = 128;
        public const int PyArray_Sort = 129;
        public const int PyArray_ArgSort = 130;
        public const int PyArray_SearchSorted = 131;
        public const int PyArray_ArgMax = 132;
        public const int PyArray_ArgMin = 133;
        public const int PyArray_Reshape = 134;
        public const int PyArray_Newshape = 135;
        public const int PyArray_Squeeze = 136;
        public const int PyArray_View = 137;// StealRef(2)
        public const int PyArray_SwapAxes = 138;
        public const int PyArray_Max = 139;
        public const int PyArray_Min = 140;
        public const int PyArray_Ptp = 141;
        public const int PyArray_Mean = 142;
        public const int PyArray_Trace = 143;
        public const int PyArray_Diagonal = 144;
        public const int PyArray_Clip = 145;
        public const int PyArray_Conjugate = 146;
        public const int PyArray_Nonzero = 147;
        public const int PyArray_Std = 148;
        public const int PyArray_Sum = 149;
        public const int PyArray_CumSum = 150;
        public const int PyArray_Prod = 151;
        public const int PyArray_CumProd = 152;
        public const int PyArray_All = 153;
        public const int PyArray_Any = 154;
        public const int PyArray_Compress = 155;
        public const int PyArray_Flatten = 156;
        public const int PyArray_Ravel = 157;
        public const int PyArray_MultiplyList = 158;
        public const int PyArray_MultiplyIntList = 159;
        public const int PyArray_GetPtr = 160;
        public const int PyArray_CompareLists = 161;
        public const int PyArray_AsCArray = 162;// StealRef(5)
        public const int PyArray_As1D = 163;
        public const int PyArray_As2D = 164;
        public const int PyArray_Free = 165;
        public const int PyArray_Converter = 166;
        public const int PyArray_IntpFromSequence = 167;
        public const int PyArray_Concatenate = 168;
        public const int PyArray_InnerProduct = 169;
        public const int PyArray_MatrixProduct = 170;
        public const int PyArray_CopyAndTranspose = 171;
        public const int PyArray_Correlate = 172;
        public const int PyArray_TypestrConvert = 173;
        public const int PyArray_DescrConverter = 174;
        public const int PyArray_DescrConverter2 = 175;
        public const int PyArray_IntpConverter = 176;
        public const int PyArray_BufferConverter = 177;
        public const int PyArray_AxisConverter = 178;
        public const int PyArray_BoolConverter = 179;
        public const int PyArray_ByteorderConverter = 180;
        public const int PyArray_OrderConverter = 181;
        public const int PyArray_EquivTypes = 182;
        public const int PyArray_Zeros = 183;// StealRef(3)
        public const int PyArray_Empty = 184;// StealRef(3)
        public const int PyArray_Where = 185;
        public const int PyArray_Arange = 186;
        public const int PyArray_ArangeObj = 187;
        public const int PyArray_SortkindConverter = 188;
        public const int PyArray_LexSort = 189;
        public const int PyArray_Round = 190;
        public const int PyArray_EquivTypenums = 191;
        public const int PyArray_RegisterDataType = 192;
        public const int PyArray_RegisterCastFunc = 193;
        public const int PyArray_RegisterCanCast = 194;
        public const int PyArray_InitArrFuncs = 195;
        public const int PyArray_IntTupleFromIntp = 196;
        public const int PyArray_TypeNumFromName = 197;
        public const int PyArray_ClipmodeConverter = 198;
        public const int PyArray_OutputConverter = 199;
        public const int PyArray_BroadcastToShape = 200;
        public const int _PyArray_SigintHandler = 201;
        public const int _PyArray_GetSigintBuf = 202;
        public const int PyArray_DescrAlignConverter = 203;
        public const int PyArray_DescrAlignConverter2 = 204;
        public const int PyArray_SearchsideConverter = 205;
        public const int PyArray_CheckAxis = 206;
        public const int PyArray_OverflowMultiplyList = 207;
        public const int PyArray_CompareString = 208;
        public const int PyArray_MultiIterFromObjects = 209;
        public const int PyArray_GetEndianness = 210;
        public const int PyArray_GetNDArrayCFeatureVersion = 211;
        public const int PyArray_Correlate2 = 212;
        public const int PyArray_NeighborhoodIterNew = 213;
        // End 1.5 API
        public const int PyArray_SetDatetimeParseFunction = 219;
        public const int PyArray_DatetimeToDatetimeStruct = 220;
        public const int PyArray_TimedeltaToTimedeltaStruct = 221;
        public const int PyArray_DatetimeStructToDatetime = 222;
        public const int PyArray_TimedeltaStructToTimedelta = 223;
        // NDIter API
        public const int NpyIter_New = 224;
        public const int NpyIter_MultiNew = 225;
        public const int NpyIter_AdvancedNew = 226;
        public const int NpyIter_Copy = 227;
        public const int NpyIter_Deallocate = 228;
        public const int NpyIter_HasDelayedBufAlloc = 229;
        public const int NpyIter_HasExternalLoop = 230;
        public const int NpyIter_EnableExternalLoop = 231;
        public const int NpyIter_GetInnerStrideArray = 232;
        public const int NpyIter_GetInnerLoopSizePtr = 233;
        public const int NpyIter_Reset = 234;
        public const int NpyIter_ResetBasePointers = 235;
        public const int NpyIter_ResetToIterIndexRange = 236;
        public const int NpyIter_GetNDim = 237;
        public const int NpyIter_GetNOp = 238;
        public const int NpyIter_GetIterNext = 239;
        public const int NpyIter_GetIterSize = 240;
        public const int NpyIter_GetIterIndexRange = 241;
        public const int NpyIter_GetIterIndex = 242;
        public const int NpyIter_GotoIterIndex = 243;
        public const int NpyIter_HasMultiIndex = 244;
        public const int NpyIter_GetShape = 245;
        public const int NpyIter_GetGetMultiIndex = 246;
        public const int NpyIter_GotoMultiIndex = 247;
        public const int NpyIter_RemoveMultiIndex = 248;
        public const int NpyIter_HasIndex = 249;
        public const int NpyIter_IsBuffered = 250;
        public const int NpyIter_IsGrowInner = 251;
        public const int NpyIter_GetBufferSize = 252;
        public const int NpyIter_GetIndexPtr = 253;
        public const int NpyIter_GotoIndex = 254;
        public const int NpyIter_GetDataPtrArray = 255;
        public const int NpyIter_GetDescrArray = 256;
        public const int NpyIter_GetOperandArray = 257;
        public const int NpyIter_GetIterView = 258;
        public const int NpyIter_GetReadFlags = 259;
        public const int NpyIter_GetWriteFlags = 260;
        public const int NpyIter_DebugPrint = 261;
        public const int NpyIter_IterationNeedsAPI = 262;
        public const int NpyIter_GetInnerFixedStrideArray = 263;
        public const int NpyIter_RemoveAxis = 264;
        public const int NpyIter_GetAxisStrideArray = 265;
        public const int NpyIter_RequiresBuffering = 266;
        public const int NpyIter_GetInitialDataPtrArray = 267;
        public const int NpyIter_CreateCompatibleStrides = 268;
        //
        public const int PyArray_CastingConverter = 269;
        public const int PyArray_CountNonzero = 270;
        public const int PyArray_PromoteTypes = 271;
        public const int PyArray_MinScalarType = 272;
        public const int PyArray_ResultType = 273;
        public const int PyArray_CanCastArrayTo = 274;
        public const int PyArray_CanCastTypeTo = 275;
        public const int PyArray_EinsteinSum = 276;
        public const int PyArray_NewLikeArray = 277;// StealRef(3), NonNull(1)
        public const int PyArray_GetArrayParamsFromObject = 278;
        public const int PyArray_ConvertClipmodeSequence = 279;
        public const int PyArray_MatrixProduct2 = 280;
        // End 1.6 API
        public const int NpyIter_IsFirstVisit = 281;
        public const int PyArray_SetBaseObject = 282;// StealRef(2)
        public const int PyArray_CreateSortedStridePerm = 283;
        public const int PyArray_RemoveAxesInPlace = 284;
        public const int PyArray_DebugPrint = 285;
        public const int PyArray_FailUnlessWriteable = 286;
        public const int PyArray_SetUpdateIfCopyBase = 287;// StealRef(2)
        public const int PyDataMem_NEW = 288;
        public const int PyDataMem_FREE = 289;
        public const int PyDataMem_RENEW = 290;
        public const int PyDataMem_SetEventHook = 291;
        public const int PyArray_MapIterSwapAxes = 293;
        public const int PyArray_MapIterArray = 294;
        public const int PyArray_MapIterNext = 295;
        // End 1.7 API
        public const int PyArray_Partition = 296;
        public const int PyArray_ArgPartition = 297;
        public const int PyArray_SelectkindConverter = 298;
        public const int PyDataMem_NEW_ZEROED = 299;
        // End 1.8 API
        // End 1.9 API
        public const int PyArray_CheckAnyScalarExact = 300;// NonNull(1)
        // End 1.10 API
        public const int PyArray_MapIterArrayCopyIfOverlap = 301;
        // End 1.13 API
        public const int PyArray_ResolveWritebackIfCopy = 302;
        public const int PyArray_SetWritebackIfCopyBase = 303;
        // End 1.14 API
    }

}
