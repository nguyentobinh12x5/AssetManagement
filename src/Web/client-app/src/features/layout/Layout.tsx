import React from "react";
import Header from "./Header";
import Sidebar from "./Sidebar";

interface Props {
  children: React.ReactNode;
  showSidebar: boolean;
}

const Layout: React.FC<Props> = ({ children, showSidebar }) => {
  return (
    <>
      <Header />
      <div className="container-lg-min container-fluid">
        <div className="row mt-5">
          {showSidebar && (
            <div
              className="col-12 col-md-3"
              style={{
                minWidth: "min-content",
                maxWidth: "19%",
              }}
            >
              <Sidebar />
            </div>
          )}

          <div className={`${showSidebar ? "col-12 col-md-9" : "col-12"}`}>
            {children}
          </div>
        </div>
      </div>
    </>
  );
};

export default Layout;
