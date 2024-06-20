import React from 'react';
import { Modal } from 'react-bootstrap';
import "./Detail.scss"
import useDetailUser from './useDetailModal';
import Loading from '../../../components/Loading';



interface PopupComponentProps {
    show: boolean;
    handleClose: () => void;
    userId:string
}
const formatDate = (date: Date) => {
  const parsedDate = typeof date === 'string' ? new Date(date) : date;
  const day = String(parsedDate.getDate()).padStart(2, '0');
  const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
  const year = parsedDate.getFullYear();
  return `${day}/${month}/${year}`;
};
const PopupComponent: React.FC<PopupComponentProps> = ({ show, handleClose, userId}) => {
	const { user} = useDetailUser(userId);
	if (!user) {
		return (
			<Modal show={show} onHide={handleClose}>
				<Loading />
			</Modal>
		);
	}
    return (
        <Modal show={show} onHide={handleClose} >
            <Modal.Body>
				<div id="detailed_user">
					<svg className="header_kxg">
						<rect id="header_kxg" rx="10" ry="10" x="0" y="0" width="480" height="64">
						</rect>
					</svg>
					<svg className="background_kxh" viewBox="0 0 480 315.713">
						<path id="background_kxh" d="M 0 0 L 480 0 L 480 307.4263610839844 C 480 312.0028381347656 475.5228576660156 315.7127685546875 470 315.7127685546875 L 10 315.7127685546875 C 4.477152347564697 315.7127685546875 0 312.0028381347656 0 307.4263610839844 L 0 0 Z">
						</path>
					</svg>
					<div id="Last_Name_kxi">
						<div id="Full_Name_kxj">
							<span>Full Name</span>
						</div>
						<svg className="Rectangle_330_kxk">
							<rect id="Rectangle_330_kxk" rx="5" ry="5" x="0" y="0" width="280" height="35">
							</rect>
						</svg>
						<div id="An_Tran_Van_kxl">
							<span>{user.firstName} {user.lastName}</span>
						</div>
					</div>
					<div id="DOB_kxm">
						<div id="Date_of_Birth_kxn">
							<span>Date of Birth</span>
						</div>
						<svg className="Rectangle_330_kxo">
							<rect id="Rectangle_330_kxo" rx="5" ry="5" x="0" y="0" width="280" height="35">
							</rect>
						</svg>
						<div id="ID10111996_kxp">
                <span>{formatDate(user.dateOfBirth)}</span>
						</div>
					</div>
					<div id="Gender_kxq">
						<div id="gender_kxr">
							<div id="female_kxs" className="female">
								<div id="Label_kxt">
									<span>{ user.gender}</span>
								</div>
							</div>
						</div>
						<div id="Gender_kxu">
							<span>Gender</span>
						</div>
					</div>
					<div id="Joined_Date_kxv">
						<div id="Joined_Date_kxw">
							<span>Joined Date</span>
						</div>
						<svg className="Rectangle_330_kxx">
							<rect id="Rectangle_330_kxx" rx="5" ry="5" x="0" y="0" width="280" height="35">
							</rect>
						</svg>
						<div id="ID10052020_kxy">
                <span>{formatDate(user.joinDate)}</span>
						</div>
					</div>
					<div id="Type_kxz">
						<div id="Type_kx">
							<span>Type</span>
						</div>
						<svg className="Path_8_kx" viewBox="0 0 280 35">
							<path id="Path_8_kx" d="M 6 0 L 275 0 C 277.7614135742188 0 280 2.238576173782349 280 5 L 280 30 C 280 32.76142501831055 277.7614135742188 35 275 35 L 5 35 C 2.238576173782349 35 0 32.76142501831055 0 30 L 0 5 C 0 2.238576173782349 3.238576173782349 0 6 0 Z">
							</path>
						</svg>
						<div id="Staff_kx">
							<span>{ user.type}</span>
						</div>
					</div>
					<div id="First_Name_kx">
						<div id="Staff_Code_kx">
							<span>Staff Code</span>
						</div>
						<svg className="Rectangle_330_kx">
							<rect id="Rectangle_330_kx" rx="5" ry="5" x="0" y="0" width="280" height="35">
							</rect>
						</svg>
						<div id="SD1234_kx">
							<span>{user.staffCode}</span>
						</div>
						<svg className="Path_15_kx" viewBox="0 0 1 1">
							<path id="Path_15_kx" d="M 0 0">
							</path>
						</svg>
					</div>
					<div id="Detailed_User_Information">
						<span>Detailed User Information</span>
					</div>
					<svg className="x-square-fill_kx" viewBox="0 0 20 20" >
						<path onClick={handleClose} id="x-square-fill_kx" d="M 2.5 0 C 1.119288086891174 0 -2.980232238769531e-07 1.119288444519043 0 2.500000238418579 L 0 17.5 C 0 18.88071250915527 1.119288206100464 20 2.5 20 L 17.5 20 C 18.88071250915527 20 20 18.88071250915527 20 17.5 L 20 2.5 C 20 1.119288086891174 18.88071250915527 0 17.5 0 L 2.5 0 Z M 6.692500114440918 5.807499885559082 L 10 9.116250038146973 L 13.30749988555908 5.807499885559082 C 13.5518856048584 5.563113689422607 13.9481143951416 5.563113689422607 14.19250011444092 5.807499885559082 C 14.43688583374023 6.051885604858398 14.43688583374023 6.448113918304443 14.19250011444092 6.692500114440918 L 10.88374996185303 10 L 14.19250011444092 13.30749988555908 C 14.43688583374023 13.5518856048584 14.43688583374023 13.9481143951416 14.19250011444092 14.19250011444092 C 13.9481143951416 14.43688583374023 13.5518856048584 14.43688583374023 13.30749988555908 14.19250011444092 L 10 10.88374996185303 L 6.692500114440918 14.19250011444092 C 6.448113441467285 14.43688583374023 6.051885604858398 14.43688583374023 5.807499408721924 14.19250011444092 C 5.563113689422607 13.9481143951416 5.563113689422607 13.55188465118408 5.80750036239624 13.30749988555908 L 9.116250038146973 10 L 5.807499885559082 6.692500114440918 C 5.563113689422607 6.448113918304443 5.563113689422607 6.051886081695557 5.807499885559082 5.807499885559082 C 6.051886081695557 5.563113689422607 6.448113918304443 5.563113689422607 6.692500114440918 5.807499885559082 Z">
						</path>
					</svg>
					<div id="Type_kya">
						<div id="Location_kyb">
							<span>Location</span>
						</div>
						<svg className="Path_8_kyc" viewBox="0 0 280 35">
							<path id="Path_8_kyc" d="M 6 0 L 275 0 C 277.7614135742188 0 280 2.238576173782349 280 5 L 280 30 C 280 32.76142501831055 277.7614135742188 35 275 35 L 5 35 C 2.238576173782349 35 0 32.76142501831055 0 30 L 0 5 C 0 2.238576173782349 3.238576173782349 0 6 0 Z">
							</path>
						</svg>
						<div id="HCM_kyd">
							<span>{ user.location}</span>
						</div>
					</div>
					<div id="Last_Name_kyh">
						<div id="Username">
							<span>Username</span>
						</div>
						<svg className="Rectangle_330_kyj">
							<rect id="Rectangle_330_kyj" rx="5" ry="5" x="0" y="0" width="280" height="35">
							</rect>
						</svg>
						<div id="antv_kyk">
							<span>{user.username }</span>
						</div>
					</div>
					</div>
            </Modal.Body>
        </Modal>
    );
};

export default PopupComponent;

