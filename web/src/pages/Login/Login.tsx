import { useNavigate } from "react-router-dom";
import { loginApi } from "../../features/login/apis/login.api";
import LoginForm from "../../features/login/components/LoginForm";
import type { LoginRequest } from "../../features/login/types/login.type";
import { message } from "antd";
import axios from "axios";

export default function LoginPage() {
    const navigate = useNavigate();

    const handleLogin = async (data: LoginRequest) => {
        try {
            await loginApi.login(data)
            navigate("/", { replace: true });
        } catch (error: unknown) {
            if (axios.isAxiosError(error)) {
                const msg = error.response?.data?.message;
                message.error(msg || "Something went wrong");
                return;
            }

            message.error("Fail, please try again!");
        }
    };

    return (
        <div className="min-h-screen bg-gray-200 flex items-center justify-center p-6">
            <div
                className="flex w-full max-w-3xl bg-white shadow-xl overflow-hidden"
                style={{
                    borderRadius: "4px 40px 4px 4px",
                    minHeight: "520px",
                }}
            >
                {/* ── Left: Form panel ── */}
                <div className="flex flex-col justify-between w-5/12 px-10 py-10">
                    <div>
                        <h1 className="text-2xl font-extrabold text-gray-800 mb-8 tracking-tight">
                            Retail System Admin
                        </h1>
                        <LoginForm onSubmit={handleLogin} />
                    </div>
                </div>

                {/* ── Right: Visual panel ── */}
                <div
                    className="flex-1 relative flex items-center justify-center overflow-hidden"
                    style={{
                        background: "linear-gradient(145deg, #eef0f8 0%, #e0e3f4 60%, #d4d8f0 100%)",
                        borderRadius: "0 38px 0 0",
                    }}
                >
                    <RetailIllustration />
                </div>
            </div>
        </div>
    );
}

function RetailIllustration() {
    return (
        <svg
            viewBox="0 0 320 340"
            width="300"
            height="320"
            xmlns="http://www.w3.org/2000/svg"
        >
            {/* ── Floating price tag (top-left) ── */}
            <g transform="translate(18, 42) rotate(-12)">
                <rect x="0" y="0" width="72" height="34" rx="6" fill="#4F46E5" opacity="0.92" />
                <circle cx="9" cy="17" r="4" fill="white" opacity="0.7" />
                <rect x="17" y="9" width="32" height="4" rx="2" fill="white" opacity="0.9" />
                <rect x="17" y="19" width="22" height="4" rx="2" fill="white" opacity="0.55" />
            </g>

            {/* ── Floating tag (top-right) ── */}
            <g transform="translate(228, 28) rotate(10)">
                <rect x="0" y="0" width="62" height="28" rx="6" fill="#818cf8" opacity="0.85" />
                <rect x="8" y="8" width="28" height="4" rx="2" fill="white" opacity="0.9" />
                <rect x="8" y="17" width="18" height="3" rx="1.5" fill="white" opacity="0.5" />
            </g>

            {/* ── Main shopping bag ── */}
            <g transform="translate(80, 88)">
                {/* Bag body */}
                <rect x="0" y="30" width="160" height="150" rx="14" fill="#4F46E5" />
                {/* Bag top rim */}
                <rect x="10" y="24" width="140" height="18" rx="8" fill="#3730a3" />
                {/* Handle left arch */}
                <path
                    d="M48 24 Q48 0 80 0 Q112 0 112 24"
                    fill="none"
                    stroke="#3730a3"
                    strokeWidth="10"
                    strokeLinecap="round"
                />
                {/* Shine strip */}
                <rect x="16" y="52" width="6" height="100" rx="3" fill="white" opacity="0.12" />
                {/* Label area */}
                <rect x="32" y="88" width="96" height="44" rx="8" fill="white" opacity="0.15" />
                <rect x="44" y="100" width="60" height="6" rx="3" fill="white" opacity="0.8" />
                <rect x="52" y="113" width="44" height="4" rx="2" fill="white" opacity="0.5" />
                {/* Subtle dots */}
                <circle cx="136" cy="148" r="4" fill="white" opacity="0.2" />
                <circle cx="24" cy="148" r="3" fill="white" opacity="0.15" />
            </g>

            {/* ── Receipt strip ── */}
            <g transform="translate(188, 148) rotate(8)">
                <rect x="0" y="0" width="68" height="90" rx="4" fill="white" opacity="0.92" />
                <path d="M0 90 Q8 84 16 90 Q24 96 32 90 Q40 84 48 90 Q56 96 64 90 L68 90 L68 94 L0 94Z" fill="white" opacity="0.92" />
                <rect x="8" y="10" width="52" height="4" rx="2" fill="#4F46E5" opacity="0.7" />
                <rect x="8" y="20" width="38" height="3" rx="1.5" fill="#a5b4fc" />
                <rect x="8" y="29" width="44" height="3" rx="1.5" fill="#a5b4fc" />
                <rect x="8" y="38" width="34" height="3" rx="1.5" fill="#a5b4fc" />
                <rect x="8" y="53" width="52" height="1" rx="0.5" fill="#c7d2fe" />
                <rect x="8" y="62" width="28" height="3" rx="1.5" fill="#6366f1" opacity="0.6" />
                <rect x="40" y="61" width="20" height="5" rx="2" fill="#4F46E5" opacity="0.85" />
            </g>

            {/* ── Barcode card (bottom-left) ── */}
            <g transform="translate(44, 196) rotate(-6)">
                <rect x="0" y="0" width="80" height="52" rx="6" fill="white" opacity="0.9" />
                <rect x="8" y="8" width="2" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="12" y="8" width="1" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="15" y="8" width="3" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="20" y="8" width="1" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="23" y="8" width="2" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="27" y="8" width="4" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="33" y="8" width="1" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="36" y="8" width="2" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="40" y="8" width="3" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="45" y="8" width="1" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="48" y="8" width="2" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="52" y="8" width="4" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="58" y="8" width="1" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="61" y="8" width="2" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="65" y="8" width="3" height="26" rx="0.5" fill="#4F46E5" opacity="0.7" />
                <rect x="8" y="38" width="64" height="3" rx="1.5" fill="#a5b4fc" opacity="0.8" />
            </g>

            {/* ── Discount coin badge ── */}
            <g transform="translate(56, 270)">
                <circle cx="22" cy="22" r="22" fill="#6366f1" />
                <circle cx="22" cy="22" r="17" fill="none" stroke="white" strokeWidth="1.5" opacity="0.4" />
                <rect x="13" y="18" width="18" height="3" rx="1.5" fill="white" opacity="0.9" />
                <rect x="16" y="24" width="12" height="3" rx="1.5" fill="white" opacity="0.6" />
            </g>

            {/* ── Star sparkles ── */}
            <polygon points="270,90 272,96 278,96 273,100 275,106 270,102 265,106 267,100 262,96 268,96" fill="#818cf8" opacity="0.6" />
            <polygon points="52,72 53.5,77 58,77 54.5,80 56,85 52,82 48,85 49.5,80 46,77 50.5,77" fill="#a5b4fc" opacity="0.5" />

            {/* ── Ground shadow ── */}
            <ellipse cx="160" cy="298" rx="88" ry="10" fill="#4F46E5" opacity="0.1" />
        </svg>
    );
}
