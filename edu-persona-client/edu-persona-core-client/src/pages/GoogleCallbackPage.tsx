import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const GoogleCallbackPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    const code = params.get("code");

    if (!code) {
      navigate("/login");
      return;
    }

    const loginWithGoogle = async () => {
      try {
        const response = await fetch(
          `${import.meta.env.VITE_API_BASE_URL}/login/google-login`,
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ code }),
          }
        );

        const result = await response.json();

        if (!result.success) {
          navigate("/login");
          return;
        }

        const sessionId = result.data.sessionId;

        // Validate session
        const validateResponse = await fetch(
          `${
            import.meta.env.VITE_API_BASE_URL
          }/login/validate-session/${sessionId}`,
          {
            credentials: "include",
          }
        );

        const validateResult = await validateResponse.json();

        // Store tokens
        localStorage.setItem("accessToken", validateResult.data.accessToken);
        localStorage.setItem("refreshToken", validateResult.data.refreshToken);

        navigate("/dashboard"); // or wherever
      } catch (error) {
        navigate("/login");
      }
    };

    loginWithGoogle();
  }, [navigate]);

  return <div>Signing you in...</div>;
};

export default GoogleCallbackPage;
