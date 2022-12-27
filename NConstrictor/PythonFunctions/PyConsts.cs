namespace NConstrictor
{
    public class PyConsts
    {
        public const int PY_LT = 0;
        public const int PY_LE = 1;
        public const int PY_EQ = 2;
        public const int PY_NE = 3;
        public const int PY_GT = 4;
        public const int PY_GE = 5;

        public const int FILE_INPUT = 257;
        public const int EVAL_INPUT = 258;

        public const int BUF_SIMPLE = 0;
        public const int BUF_WRITABLE = 0x0001;
        public const int BUF_FORMAT = 0x0004;
        public const int BUF_ND = 0x0008;
        public const int BUF_STRIDES = (0x0010 | BUF_ND);
        public const int BUF_C_CONTIGUOUS = (0x0020 | BUF_STRIDES);
        public const int BUF_F_CONTIGUOUS = (0x0040 | BUF_STRIDES);
        public const int BUF_ANY_CONTIGUOUS = (0x0080 | BUF_STRIDES);
        public const int BUF_INDIRECT = (0x0100 | BUF_STRIDES);

        public const int BUF_CONTIG = (BUF_ND | BUF_WRITABLE);
        public const int BUF_CONTIG_RO = (BUF_ND);

        public const int BUF_STRIDED = (BUF_STRIDES | BUF_WRITABLE);
        public const int BUF_STRIDED_RO = (BUF_STRIDES);

        public const int BUF_RECORDS = (BUF_STRIDES | BUF_WRITABLE | BUF_FORMAT);
        public const int BUF_RECORDS_RO = (BUF_STRIDES | BUF_FORMAT);

        public const int BUF_FULL = (BUF_INDIRECT | BUF_WRITABLE | BUF_FORMAT);
        public const int BUF_FULL_RO = (BUF_INDIRECT | BUF_FORMAT);


        // buffertype
        public const int BUF_READ = 0x100;
        public const int BUF_WRITE = 0x200;
        public const int BUF_SHADOW = 0x400;

        internal const int NPY_MAXDIMS = 32;
        internal const int NPY_MAXARGS = 32;
    }

    public enum NPY_CLIPMODE
    {
        CLIP = 0,
        WRAP = 1,
        RAISE = 2
    }
}
